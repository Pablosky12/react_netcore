import React from "react";
const Table = ({ headers, data }) => {
  return (
    <table className="table table-striped">
      <thead>
        <tr>
          {headers.map(h => {
            return <th> {h} </th>;
          })}
        </tr>
      </thead>
      <tbody>
        {data.map(data =>
          <tr key={data.id}>
            <td>
              {data.make.name}
            </td>
            <td>
              {data.model.name}
            </td>
            <td>
              {data.contact.name}
            </td>
            <td>
              {data.isRegistered}
            </td>
            <td>
              {data.features.length > 0 ? data.features.map(x => x.name).reduce((prev, curr) => prev + ' ' + curr) : ''}
            </td>
          </tr>
        )}
      </tbody>
    </table>
  );
};


export default Table;