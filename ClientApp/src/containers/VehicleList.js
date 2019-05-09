import React from "react";
import Table from "../components/Table";
import { LabeledItem } from "../components/LabeledItem";
import { Field } from "formik";
import axios from "axios";
const baseUrl = "https://localhost:5001/api";

export default class VehicleList extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      allVehicles: [],
      filteredVehicles: [],
      makes: [],
      selectedMake: []
    };
    this.handleChange = this.handleChange.bind(this);
  }

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

  handleChange(e) {
    const value = e.target.value;
    if (value != -1) {
      //   filteredVehicles = this.state.allVehicles.filter(
      //     x => x.make.id == e.target.value
      //   );
      //   this.setState({ filteredVehicles });
      // } else {
      //   this.setState({ filteredVehicles: this.state.allVehicles });
      axios
        .get(baseUrl + "/vehicles", {
          params: {
            makeId: e.target.value
          }
        })
        .then(x => {
          this.setState({
            allVehicles: x.data,
            filteredVehicles: x.data
          });
        });
    } else {
      axios.get(baseUrl + "/vehicles").then(x => {
        this.setState({
          allVehicles: x.data,
          filteredVehicles: x.data
        });
      });
    }
  }

  render() {
    const { filteredVehicles, makes } = this.state;
    return (
      <React.Fragment>
        <LabeledItem label="Makes">
          <select onChange={this.handleChange}>
            {makes
              ? makes.map(x =>
                  <option value={x.id} key={x.id}>
                    {x.name}
                  </option>
                )
              : null}
          </select>
        </LabeledItem>
        <Table headers={[1, 2, 3, 4]} data={filteredVehicles} />
      </React.Fragment>
    );
  }
}
