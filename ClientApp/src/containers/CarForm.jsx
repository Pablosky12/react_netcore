import React, { Component } from 'react';
import { Form, FormGroup } from 'reactstrap'
import LabeledInput from '../components/LabeledInput';
import RadioButtonGroup from '../components/RadioButtonGroup';
import Select from '../components/Select';
import Checkbox from '../components/Checkbox';
export class CarForm extends Component {
  inputFields = {
    ContactPhone: 'Phone',
    ContactName: 'Name',
    ContactEmail: 'Email',
  };

  radioButtonOptions;
  componentDidMount() {
    Promise.all([
      fetch("https://localhost:5001/api/makes"),
      fetch("https://localhost:5001/api/features"),
    ]).then(([makes, features]) => {
      makes.json().then(x => this.setState({ Makes: x }));
      features.json().then(x => this.setState({ Features: x }));
    })
  }

  constructor(props) {
    super(props);
    this.state = {
      ContactPhone: '',
      ContactName: '',
      ContactEmail: '',
      VehicleRegistered: '',
      Makes: [],
      Models: [],
      Features: [],
      SelectedMake: '',
      SelectedModel: '',
    }
    this.handleChange = this.handleChange.bind(this);
    this.handleModelChange = this.handleModelChange.bind(this);
    this.handleCheckboxChange = this.handleCheckboxChange.bind(this);
    this.radioButtonOptions = [
      { disabled: false, value: 'true', checked: this.state.VehicleRegistered == 'true', label: 'Yes' },
      { disabled: false, value: 'false', checked: this.state.VehicleRegistered == 'false', label: 'No' }
    ];
  }

  handleChange(controlName, event) {
    this.setState({
      [controlName]: event.target.value
    })
    console.log(this.state);
  }

  handleModelChange(event) {
    this.setState({
      SelectedMake: event.target.value,
      Models: this.state.Makes.find(x => x.id == event.target.value).models
    })
  }

  handleCheckboxChange(controlName, event) {
    const updatedFeatures = this.state.Features[this.state.Features.find(event.targe)]
    this.setState({
      [controlName]: event.target.checked
    })
  }

  renderInputs() {
    let inputs = [];
    for (const field in this.inputFields) {
      inputs.push(
        <LabeledInput name={field}
          label={this.inputFields[field]}
          placeholder={this.inputFields[field]}
          id={field}
          onChange={this.handleChange.bind(null, field)}
          value={this.state[field]}
          key={field}>
        </LabeledInput>
      )
    }
    return inputs;
  }


  render() {
    return (
      <Form>
        <legend>Vehicle Information</legend>
        <Select options={this.state.Makes}
          label="Make"
          multiple={false}
          name="Makes"
          onChange={this.handleModelChange}>
        </Select>
        <Select options={this.state.Models}
          label="Model"
          multiple={false}
          name="Models"
          onChange={this.handleChange.bind(null, 'SelectedModel')}>
        </Select>
        <RadioButtonGroup options={this.radioButtonOptions}
          legend="Is this vehicle registered?"
          name="VehicleRegistered"
          onChange={this.handleChange.bind(null, 'VehicleRegistered')}>
        </RadioButtonGroup>

        <legend>Features</legend>
        {
          this.state.features.map(feature => {
            return  (
              <Checkbox name={'Features'}
                        label={feature.Name}
                        checked={feature.checked}
                        onChange={this.handleCheckboxChange.bind(null, 'Features')} />
            )
          })
        }
        <legend>Contact Information</legend>

        <FormGroup>
          {this.renderInputs().map(x => x)}
        </FormGroup>
      </Form>
    );
  }
}
