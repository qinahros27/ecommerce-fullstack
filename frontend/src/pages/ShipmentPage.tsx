import HeaderBar from "./components/HeaderBar";
import PageFooter from "./components/PageFooter";
import avatar from "../images/avatar.png";
import useAppDispatch from "../hooks/useAppDispatch";
import useAppSelector from "../hooks/useAppSelector";

const ShipmentPage = () => {
  const dispatch = useAppDispatch();
  const {shipments} = useAppSelector((state) => state.shipmentsReducer);
  return (
    <div>
      <HeaderBar />
      <div className="Shipment-container">
        <div className="Shipment-inside">
          <h1>Your Orders</h1>
          <div className="Shipment-items-container">
            <div className="Shipment-items-box">
              <div className="Shipment-items-header">
                <div className="Shipment-items-header-component">
                  <h4>Order place</h4>
                  <p>2 Jun 2023</p>
                </div>
                <div className="Shipment-items-header-component">
                  <h4>Total</h4>
                  <p>&euro; 59.40</p>
                </div>
                <div className="Shipment-items-header-component detail">
                  <h4>Order #1234-1234-12345678912</h4>
                  <p>
                    <a href="#">View details</a>
                  </p>
                </div>
              </div>

              <div className="Shipment-items-product">
                <img src={avatar} alt="default avatar" />
                <div className="Shipment-items-product-detail">
                  <h4>Book Learning Mandarine</h4>
                  <p>
                    <strong>Shipment state: </strong>Delivering
                  </p>
                  <p>
                    <strong>Shipment tracking number: </strong>{" "}
                    00370727790020930251
                  </p>
                </div>
                <div className="Shipment-items-product-price">
                  <p>&euro; 20.00</p>
                </div>
              </div>

              <div className="Shipment-items-footer">
                <a href="#">Archive order</a>
              </div>
            </div>
          </div>
        </div>
      </div>
      <PageFooter />
    </div>
  );
};

export default ShipmentPage;
