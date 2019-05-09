import React from "react";
import styled from "styled-components";
const StyledLabel = styled.label`
  font-weight: 600;
  display: flex;
  margin: 0;
`;
export const LabeledItem = ({ label, children, htmlFor }) => {
  return (
    <React.Fragment>
      <StyledLabel htmlFor={htmlFor}>
        {label}
      </StyledLabel>
      {children}
    </React.Fragment>
  );
};
