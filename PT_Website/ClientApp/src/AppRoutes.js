import { Counter } from "./components/Counter";
import { FetchData } from "./components/FetchData";
import { Home } from "./components/Home";
import { GioiThieu } from "./components/GioiThieu";
import { GetProductDetail } from "./components/ProductDetail";

const AppRoutes = [
  {
    index: true,
    element: <Home />
  },
  {
    path: '/counter',
    element: <Counter />
  },
  {
    path: '/fetch-data',
    element: <FetchData />
    },
    {
        path: '/gioi-thieu',
        element: <GioiThieu/>
    }
    ,
    {
        path: '/San-Pham',
        element: <GetProductDetail />
    }
];

export default AppRoutes;
