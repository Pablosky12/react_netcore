import React from "react";
import { Form, Field, Formik } from "formik";
import { LabeledField } from "../components/LabeledField";
import styled from "styled-components";
import { LabeledItem } from "../components/LabeledItem";

const StyledForm = styled(Form)`
  display: flex;
  flex: 0 1 100;
  flex-direction: column;
  align-items: center;
  margin:10px 0;
  
  input, select {
    background: #f4f4f8;
    border: 1px solid #b3b3c1;
    padding: 6px;
    border-radius: 10px;
    width: 33%;
    margin-bottom: 10px;
    &:focus {
      outline: none;
      background: transparent;
    }
  }
`;

const StyledButton = styled.button`
  padding: 6px;
  font-weight: 600;
  border-radius: 10px;
  text-transform: uppercase;
  border-width: 0;
  &:focus {
    outline: none;
  }
`;
export class VehicleForm extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      makes: [],
      models: [],
      features: []
    };
  }
  componentDidMount() {
    Promise.all([
      fetch("https://localhost:5001/api/makes"),
      fetch("https://localhost:5001/api/features")
    ]).then(([makes, features]) => {
      makes.json().then(x => this.setState({ makes: x }));
      features.json().then(x => this.setState({ features: x }));
    });
  }

  getModels(formValues) {
    const { makes } = this.state;
    if (makes.length > 0) {
      let make = makes.find(x => x.id == formValues.SelectedMake);
      if (make) {
        return make.models
          .map(x =>
            <option value={x.id} key={x.id}>
              {x.name}
            </option>
          )
          .slice();
      }
    }
    return <React.Fragment />;
  }

  render() {
    const { makes, features } = this.state;
    const { initialValues, handleSubmit } = this.props;
    return (
      <Formik
        initialValues={initialValues}
        onSubmit={handleSubmit}
        enableReinitialize={true}
        render={({ values, setFieldValue }) =>
          <StyledForm>
            <legend> Vehicle Form</legend>
            <LabeledField
              label="Contact Phone"
              name="ContactPhone"
              type="text"
            />
            <LabeledField label="Contact Name" name="ContactName" type="text" />
            <LabeledField
              label="Contact Email"
              name="ContactEmail"
              type="text"
            />
            <LabeledItem
              label="Is Vehicle Registered"
              htmlFor="IsVehicleRegistered"
            >
              <Field
                type="Checkbox"
                id="IsVehicleRegistered"
                name="IsVehicleRegistered"
              />
            </LabeledItem>
            <LabeledItem label="Makes">
              <Field component="select" name="SelectedMake">
                {makes.map(x =>
                  <option value={x.id} key={x.id}>
                    {x.name}
                  </option>
                )}
              </Field>
            </LabeledItem>
            <LabeledItem label="Models">
              <Field component="select" name="SelectedModel">
                {this.getModels(values)}
              </Field>
            </LabeledItem>
            <LabeledItem label="Features">
              <Field
                component="select"
                name="Features"
                onChange={evt =>
                  setFieldValue(
                    "Features",
                    [].slice
                      .call(evt.target.selectedOptions)
                      .map(option => option.value)
                  )}
                multiple={true}
              >
                {features
                  .map(({ name, id }) =>
                    <option key={`${id}${name}`} value={id}>
                      {name}
                    </option>
                  )
                  .slice()}
              </Field>
            </LabeledItem>
            <StyledButton type="submit">go!</StyledButton>
          </StyledForm>}
      />
    );
  }
}

export function createResourceFromFormValues(formValues) {
  return {
    modelId: formValues.SelectedModel,
    isRegistered: formValues.IsVehicleRegistered,
    contact: {
      name: formValues.ContactName,
      phone: formValues.ContactPhone,
      email: formValues.ContactEmail
    },
    features: formValues.Features
  };
}
