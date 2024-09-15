import { RouterProvider, createBrowserRouter } from 'react-router-dom';

import SignUp from './pages/SignUp';
import Home from './pages/Home';
import NotFound from './pages/NotFound';
import ProductDetail from './pages/ProductDetail';
import LogIn from './pages/LogIn';
import OrderSuccess from './pages/OrderSuccess';
import AccountSetting from './pages/AccountSetting';
import CartPage from './pages/CartPage';
import ManageProduct from './pages/ManageProduct';
import ShipmentPage from './pages/ShipmentPage';
import PaymentPage from './pages/PaymentPage';

const router = createBrowserRouter([
    {
      path: "/",
      element: <Home />,
      errorElement: <NotFound />,
    },
    {
      path: "/signup",
      element: <SignUp />
    },
    {
      path: "/login",
      element: <LogIn />
    },
    {
      path: "/product/:id",
      element: <ProductDetail />
    },
    {
      path: "/order/success",
      element: <OrderSuccess />
    },
    {
      path: "/order/payment",
      element: <PaymentPage />
    },
    {
      path: "/account",
      element: <AccountSetting />
    },
    {
      path: "/cart",
      element: <CartPage />
    },
    {
      path: "/product/manage",
      element: <ManageProduct />
    },
    {
      path: "/your-orders",
      element: <ShipmentPage />
    }
])

const App = () => {
  return (
    <RouterProvider router={router} />
  )
}

export default App