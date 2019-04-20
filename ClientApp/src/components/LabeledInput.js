import React, {
  Component
} from 'react';
import PropTypes from 'prop-types'
import { Label, Input } from 'reactstrap'
export default class LabeledInput extends Component {
  render() {
    return (
      <span>
        <Label for={this.props.id}>
          {this.props.label}
        </Label>
        <Input type={this.props.type}
          name={this.props.name}
          id={this.props.id}
          placeholder={this.props.placeholder}
          onChange={this.props.onChange}
          value={this.props.value}/>
      </span>
    );
  }
}

LabeledInput.propTypes = {
  label: PropTypes.string,
  type: PropTypes.string,
  id: PropTypes.string,
  placeholder: PropTypes.string,
  name: PropTypes.string,
  onChange: PropTypes.func,
  value: PropTypes.string || PropTypes.number,
}
