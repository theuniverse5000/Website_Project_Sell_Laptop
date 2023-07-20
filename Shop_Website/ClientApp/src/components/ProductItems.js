import React, { useState, useEffect} from 'react';
const ProductItems = () => {
    const [items, setItems] = useState([]);
   
    useEffect(() => {
        fetch(`'api/Product'`)
            .then((results) => {
                return results.json();
            })
            .then(data => {
                setItems(data);
            })

    },[]);
    return (
        <main>
            {
                (items != null) ? items.map((pro) => <h2>{pro.Name}</h2>) : <div>No data</div>
                
            }

        </main>
    )
}

export default ProductItems;
//import React, { useEffect, useState } from 'react';
//import axios from 'axios';
//import { Table } from 'react-bootstrap';

//const TableComponent = () => {
//  const [data, setData] = useState([]);

//  useEffect(() => {
//    fetchData();
//  }, []);

//  const fetchData = async () => {
//    try {
//        const response = await axios.get('https://localhost:44333/api/Product'); // Thay đổi URL API tùy theo ứng dụng của bạn
//      setData(response.data);
//    } catch (error) {
//      console.error(error);
//    }
//  };

//  return (
//    <Table striped bordered hover>
//      <thead>
//        <tr>
//          <th>Column 1</th>
//          <th>Column 2</th>
//          {/* Thay đổi các tiêu đề cột tuỳ theo API của bạn */}
//        </tr>
//      </thead>
//      <tbody>
//        {data.map((item) => (
//          <tr key={item.id}>
//            <td>{item.column1}</td>
//            <td>{item.column2}</td>
//            {/* Thay đổi cách hiển thị dữ liệu tuỳ theo API của bạn */}
//          </tr>
//        ))}
//      </tbody>
//    </Table>
//  );
//};

//export default TableComponent;
