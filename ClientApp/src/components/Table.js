import React from "react";
const Table = ({ columns, data, headerClickFn, sorting }) => {
  return (
    <table className="table table-striped">
      <thead>
        <tr>
          {columns.map(h => {
            return (
              <th
                key={h.name}
                onClick={headerClickFn ? () => headerClickFn(h) : null}
              >
                <a>
                  {h.name}
                </a>
                &nbsp;
                {sorting.by == h.name
                  ? sorting.sortAscending ? "↑" : "↓"
                  : null}
              </th>
            );
          })}
        </tr>
      </thead>
      <tbody>
        {data.items
          ? data.items.map(item =>
              <tr key={item.id}>
                {columns.map(col =>
                  <td key={item.id + col.name}>
                    {col.getPropFn(item)}
                  </td>
                )}
              </tr>
            )
          : null}
      </tbody>
    </table>
  );
};

export default Table;
