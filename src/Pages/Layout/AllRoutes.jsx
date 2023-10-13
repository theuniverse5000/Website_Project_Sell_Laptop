import React from "react";
import { Route, Routes } from "react-router-dom";
import MainCartPage from "../cartPage/MainCartPage";
import Home from "../Home/Home";
import Login from "../Login/Login";
import ProductDetail from "../ProductDetail/ShowProductDetail";
import SingleProduct from "../SingleProduct/SingleProduct";
import Wishlist from "../Wishlist/Wishlist";
import SearchPage from "../SearchPage/SearchPage";
import Payments from "../payment/Payments";
import Checkout from "../checkout/checkout";
import { LastPage } from "../cartPage/LastPage";
import Signup from "../Signup/Signup";
import PrivateRoute from "../../Components/PrivateRoute/PrivateRoutes";
import { ShowListProductDetails } from "../ProductDetail/ShowListProductDetail";  
import {ShowProduct} from "../ProductDetail/ShowProductDetail"

const AllRoutes = () => {
  return (
    <div>
      <Routes>
        <Route path="/" element={<Home />}></Route>
        <Route path="/sanpham" element={<ShowListProductDetails/>} />
        <Route path="San-Pham-Chi-Tiet/:code" element={<ShowProduct/>} />
        <Route path="/cart" element={<PrivateRoute><MainCartPage /></PrivateRoute>}></Route>
        <Route path="/login" element={<Login />}></Route>
        <Route
          path="/whishlist"
          element={<PrivateRoute><Wishlist typeOfProduct={"whishlist"} /></PrivateRoute>}
        ></Route>
        <Route path="/checkout" element={<PrivateRoute><Checkout /></PrivateRoute>}></Route>
        <Route path="/payments" element={<PrivateRoute><Payments /></PrivateRoute>}></Route>
        <Route path="/lastpage" element={<PrivateRoute><LastPage /></PrivateRoute>}></Route>
        <Route path="/signup" element={<Signup />}></Route>
        {/* <Route path="/order" element={<Products typeOfProduct={"order"}/>}></Route>
            <Route path="/contactus" element={<Products typeOfProduct={"contactus"}/>}></Route>
            <Route path="/profile" element={<Products typeOfProduct={"profile"}/>}></Route> */}
      </Routes>
    </div>
  );
};
export default AllRoutes;