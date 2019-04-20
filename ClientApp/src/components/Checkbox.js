import React, { Component } from 'react';
import { Label, FormGroup, Input } from 'reactstrap'
import PropTypes from 'prop-types';
export default class Checkbox extends Component {
  render() {
    return (
      <FormGroup check>
        <Label check>
          <Input type="checkbox" name={this.props.name} onChange={this.props.onChange} checked={this.props.checked} />
          {this.props.label}
        </Label>
      </FormGroup>
    );
  }
}

Checkbox.propTypes = {
  name: PropTypes.string,
  onChange: PropTypes.func,
  checked: PropTypes.bool,
  label: PropTypes.string
}
