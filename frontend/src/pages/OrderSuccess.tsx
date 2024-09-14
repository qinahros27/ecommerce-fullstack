import PageFooter from "./components/PageFooter";
import HeaderBar from "./components/HeaderBar";

const OrderSuccess = () => {
    const date = new Date();
    date.setDate(date.getDate() + 2);

    return (
    <div>
        <HeaderBar/>
        <div className="OrderSuccess">
            <h2>Thank you for shopping with us. Sahara has received you order. You can view the status of your order or make any changes to it on <a href="/notfound">Your Orders</a> on Sahara.com </h2>
            <div className="OrderSuccess__info">
                <p>Your estimated delivery data is: </p>
                <p><strong>{date.toDateString()}</strong></p>
            </div>
        </div>
        <PageFooter/>
    </div>
    )
}
    
export default OrderSuccess