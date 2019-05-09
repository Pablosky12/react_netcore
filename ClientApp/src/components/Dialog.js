import React from "react";
import ReactDOM from "react-dom";
import styled from "styled-components";

let StyledDiv = styled.div`
  position: absolute;
  top: 0;
  right: 0;
  width: 100%;
  height: 100%;
  background: #00000030;
  display: flex;
  flex: 1 0 100%;
  justify-items: center;
  align-items: center;
  justify-content: center;
`;
const DialogContainer = styled.div`
  min-width: 600px;
  min-height: 400px;
  background-color: white;
  color: black;
  display: absolute;
  opacity: 1;
  display: flex;
  align-content: center;
  flex-direction: column;
  .title {
    font-weight: 600;
    align-self: center;
  }
`;
const DialogContent = ({ children, text, title, close, closeActionName }) => {
  return (
    <DialogContainer>
      <div className="title">
        {title}
      </div>
      {children}
      <button onClick={close}>
        {closeActionName ? closeActionName : "Close"}
      </button>
    </DialogContainer>
  );
};
export const Dialog = ({ children, ...props }) => {
  return ReactDOM.createPortal(
    <StyledDiv>
      <DialogContent {...props}>{children}</DialogContent>,
    </StyledDiv>,
    document.getElementById("modal-overlay")
  );
};
