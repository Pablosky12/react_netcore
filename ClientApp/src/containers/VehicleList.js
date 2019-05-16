import React from "react";
import Table from "../components/Table";
import { LabeledItem } from "../components/LabeledItem";
import axios from "axios";
const baseUrl = "https://localhost:5001/api";

export default class VehicleList extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      allVehicles: [],
      filteredVehicles: [],
      makes: [],
      selectedMake: -1,
      searchInput: "",
      sortBy: "",
      sortAscending: true
    };
    this.handleOptionChange = this.handleOptionChange.bind(this);
    this.sortColumns = this.sortColumns.bind(this);
  }

  columns = [
    new Column("Make", item => item.make.name, "make"),
    new Column("Model", item => item.model.name, "model"),
    new Column("Contact", item => item.contact.name, "contactName"),
    new Column(
      "Features",
      item =>
        item.features.length > 0
          ? item.features
              .map(x => x.name)
              .reduce((prev, curr) => prev + " " + curr)
          : ""
    )
  ];
  componentDidMount() {
    axios.get(baseUrl + "/makes").then(x => {
      this.setState({ makes: [{ id: -1, name: "All" }, ...x.data] });
    });
    axios.get(baseUrl + "/vehicles").then(x => {
      this.setState({
        allVehicles: x.data,
        filteredVehicles: x.data
      });
    });
  }

  handleOptionChange(e) {
    const value = e.target.value;
    const params =
      value != -1
        ? {
            makeId: e.target.value
          }
        : null;
    axios.get(baseUrl + "/vehicles", { params }).then(x => {
      this.setState({
        allVehicles: x.data,
        filteredVehicles: x.data,
        selectedMake: value
      });
    });
  }

  sortColumns({ name, sortName }) {
    const { sortAscending, selectedMake } = this.state;
    const params = {
      isSortAscending: !sortAscending,
      sortBy: sortName,
      makeId: selectedMake != -1 ? selectedMake : null
    };
    axios.get(baseUrl + "/vehicles", { params }).then(x => {
      this.setState({
        sortBy: name,
        sortAscending: !sortAscending,
        filteredVehicles: x.data
      });
    });
  }
  render() {
    const { filteredVehicles, makes, sortBy, sortAscending } = this.state;
    return (
      <React.Fragment>
        <LabeledItem label="Makes">
          <select onChange={this.handleOptionChange}>
            {makes
              ? makes.map(x =>
                  <option value={x.id} key={x.id}>
                    {x.name}
                  </option>
                )
              : null}
          </select>
        </LabeledItem>
        <Table
          columns={this.columns}
          data={filteredVehicles}
          headerClickFn={this.sortColumns}
          sorting={{ by: sortBy, sortAscending: sortAscending }}
        />
      </React.Fragment>
    );
  }
}

const Column = function(name, getPropFn, sortName) {
  this.name = name;
  this.getPropFn = getPropFn;
  this.sortName = sortName;
};
