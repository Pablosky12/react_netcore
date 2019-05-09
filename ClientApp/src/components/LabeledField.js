import React from "react";
import {Field} from "formik";
import { LabeledItem } from "../components/LabeledItem";

export const LabeledField = ({label, name, type}) => {
  return (
    <LabeledItem label={label} htmlFor={name}>
      <Field type={type} name={name} />
    </LabeledItem>
  );
}

