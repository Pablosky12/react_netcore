import React from "react";
import { VehicleForm, createResourceFromFormValues } from "./VehicleForm";
import {Dialog} from '../components/Dialog';
import axios from "axios";

export class UpdateVehicleForm extends React.Component {
  initialValues = {
    ContactPhone: "",
    ContactName: "",
    ContactEmail: "",
    IsVehicleRegistered: false,
    SelectedMake: "",
    SelectedModel: "",
    Features: [],
  }
  constructor(props) {
    super(props);
    this.state = {
      ShowModal: false
    };
    this.handleSubmit = this.handleSubmit.bind(this);
  }

  handleSubmit(id, values) {
    const url = "https://localhost:5001/api/vehicles/" + id;
    const body = createResourceFromFormValues(values);
    axios.put(url, body).then(x => {
      this.setState({ ShowModal: true });
    });
  }

  async getVehicle(id) {
    let form = {};
    if (id) {
      await axios
        .get("https://localhost:5001/api/vehicles/" + id)
        .then(result => {
          const { data, status } = result;
          if (status == 200) {
            this.initialValues = {
              ContactPhone: data.contact.phone ? data.contact.phone : "",
              ContactName: data.contact.name ? data.contact.name : "",
              ContactEmail: data.contact.email ? data.contact.email : "",
              IsVehicleRegistered: data.isRegistered,
              SelectedMake: data.make.id,
              SelectedModel: data.model.id,
              Features: data.features.map(x => x.id)
            };
            this.forceUpdate();
          }
        });
    }
    // return form;
  }
  componentDidMount() {
    let {match} = this.props
    this.getVehicle(match.params.id);
  }
  render() {
    const { match } = this.props;
    const { ShowModal } = this.state;
    return (
      <React.Fragment>
        <VehicleForm
          initialValues={this.initialValues}
          handleSubmit={this.handleSubmit.bind(null, match.params.id)}
        />
        {ShowModal ? <Dialog title="Success" close={() => this.setState({ShowModal: false})}> The vehicle was updated succesfully</Dialog> : null}        
      </React.Fragment>
    );
  }
}
