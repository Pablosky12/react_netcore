import React, { Component } from 'react';
import { Label, FormGroup, Input } from 'reactstrap';
import PropTypes from 'prop-types';

export default class Select extends Component {
  render() {
    return (
      <FormGroup>
        <Label>{this.props.label}</Label>
        <Input type="select" name={this.props.name} id={this.props.id} multiple={this.props.multiple} onChange={this.props.onChange}>
          {this.props.options.map(option => {
            return (
              <option key={option.id} value={option.id}>
                {option.name}
              </option>
            )
          })}
        </Input>
      </FormGroup>
    );
  }
}

Select.propTypes = {
  label: PropTypes.string,
  options: PropTypes.array,
  name: PropTypes.string,
  id: PropTypes.string,
  multiple: PropTypes.bool,
  onChange: PropTypes.func
}
