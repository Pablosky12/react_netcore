import React, { Component } from "react";
import { Route } from "react-router";
import { Layout } from "./components/Layout";
import { Home } from "./components/Home";
import { FetchData } from "./components/FetchData";
import { Counter } from "./components/Counter";
import { NewVehicleForm } from "./containers/NewVehicle";
import { UpdateVehicleForm } from "./containers/UpdateVehicle";
import VehicleList from "./containers/VehicleList";

export default class App extends Component {
  static displayName = App.name;

  render() {
    return (
      <React.Fragment>
        <Layout>
          <Route exact path="/" component={Home} />
          <Route exact path="/vehicle" component={NewVehicleForm} />
          <Route exact path="/vehicle/:id" component={UpdateVehicleForm} />
          <Route exact path="/vehicles" component={VehicleList} />
        </Layout>
        <div id="modal-overlay" />
      </React.Fragment>
    );
  }
}
