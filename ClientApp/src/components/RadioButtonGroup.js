import React, { Component } from 'react';
import { FormGroup, Input, Label, Col, Row } from 'reactstrap'
import PropTypes from 'prop-types'
export default class RadioButtonGroup extends Component {
  render() {
    return (
      <div>
        <label>{this.props.legend}</label>
        <Row>
          {this.props.options.map(option => {
            return (
              <Col key={option.value}>
                <div >
                  <FormGroup>
                    <Label check>
                      <Input type="radio"
                        name={this.props.name}
                        disabled={option.disabled}
                        value={option.value}
                        onChange={this.props.onChange}
                        checked={option.checked}>
                      </Input>
                      {option.label}
                    </Label>
                  </FormGroup>
                </div>
              </Col>
            )
          })}
        </Row>
      </div>

    )
  }
}

RadioButtonGroup.propTypes = {
  legend: PropTypes.string,
  name: PropTypes.string,
  options: PropTypes.array,
  onChange: PropTypes.func
}
