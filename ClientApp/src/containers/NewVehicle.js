import React from "react";
import { VehicleForm, createResourceFromFormValues } from "./VehicleForm";
import axios from "axios";
import { Dialog } from "../components/Dialog";

export class NewVehicleForm extends React.Component {
  constructor(props) {
    super(props);
    this.state = { showModal: false };
    this.handleSubmit = this.handleSubmit.bind(this);
  }
  initialValues = {
    ContactPhone: "",
    ContactName: "",
    ContactEmail: "",
    IsVehicleRegistered: false,
    SelectedMake: 1,
    SelectedModel: 1,
    Features: []
  };

  handleSubmit(values) {
    const url = "https://localhost:5001/api/vehicles";
    const body = createResourceFromFormValues(values);
    axios.post(url, body).then(x => {
      this.setState({ showModal: true });
    });
  }
  render() {
    const { showModal } = this.state;
    return (
      <React.Fragment>
        <VehicleForm
          handleSubmit={this.handleSubmit}
          initialValues={this.initialValues}
        />
        {showModal ? <Dialog title="Success" close={() => this.setState({showModal: false})}> The vehicle was saved succesfully</Dialog> : null}
      </React.Fragment>
    );
  }
}
