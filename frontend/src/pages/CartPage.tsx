import {useEffect, useState} from 'react';
import { useNavigate } from "react-router-dom";
import Checkbox from '@mui/material/Checkbox';
import FormControlLabel from '@mui/material/FormControlLabel';
import CheckCircleOutlineIcon from '@mui/icons-material/CheckCircleOutline';
import CheckCircleIcon from '@mui/icons-material/CheckCircle';

import PageFooter from "./components/PageFooter";
import HeaderBar from "./components/HeaderBar";
import useAppSelector from '../hooks/useAppSelector';
import { updateQuantity,deleteItem, emptyCartReducer} from "../redux/reducers/cartReducer";
import useAppDispatch from '../hooks/useAppDispatch';
import Cart from "../types/Cart";
import { Guid } from 'guid-typescript';

const CartPage= () => {
    const {user} = useAppSelector(state => state.userReducer);
    const {cart} = useAppSelector(state => state.cartReducer);
    const navigate = useNavigate();
    const dispatch = useAppDispatch();
    const deleteitem = (id: number) => {
        let c:Cart|undefined = cart.find(c => c.id === id)
        if(c !== undefined) {
            dispatch(deleteItem(c));
        }
    }

    const Aftercheckout = () => {
        if(user) {
            if(user.firstName === '') {
                navigate('/login');
            }
            else {
                navigate('/order/payment');
            }
        }
    }

    const emptyCart = () => {
        dispatch(emptyCartReducer());
    }

    const handleQuantityChange = (e: React.ChangeEvent<HTMLSelectElement>, id: number) => {
        dispatch(updateQuantity({id: id, quantities: parseInt(e.target.value)}))
    }

    const [checkedItems, setCheckedItems] = useState<{ [key: number]: boolean }>({});

    const handleCheckboxChange = (itemId: number) => {
        setCheckedItems((prevCheckedItems) => ({
        ...prevCheckedItems,
        [itemId]: !prevCheckedItems[itemId]
        }));
        if(!checkedItems[itemId]){
            for(let i=0 ; i<cart.length; i++) {
                if(cart[i].id == itemId)
                setTotal(total + (cart[i].product.price*cart[i].quantities));
            }
        }
        else if (checkedItems[itemId]) {
            for(let i=0 ; i<cart.length; i++) {
                if(cart[i].id == itemId)
                setTotal(total - (cart[i].product.price*cart[i].quantities));
            }
        }
    };

    const [total,setTotal] = useState(0);
    // useEffect(() => {
    //     let t = 0;
    //     if(cart.length === 0) {
    //         setTotal(0);
    //     }
    //     else {
    //         for(let i=0 ; i<cart.length; i++) {
    //             t = t + (cart[i].quantities * cart[i].product.price);
    //             setTotal(t);
    //         }
    // }
    // },[cart])
    
    return (
        <div>
          <HeaderBar/>
          <div className="CartPage">
            <div className="CartPage__inner">
                <div className="CartPage__products">
                    <div className="CartPage__products_header">
                        <h2>Shopping basket</h2>
                        {cart.length > 0 &&
                            <button onClick={() => emptyCart()}>Clear all items</button> 
                        }    
                    </div>

                    {cart.map((c) => (
                    <div className="CartPage__products_detail" key={c.id}>
                        <FormControlLabel
                        control={
                        <Checkbox
                            checked={checkedItems[c.id] || false}
                            onChange={() => handleCheckboxChange(c.id)}
                            icon={<CheckCircleOutlineIcon />} // Unchecked icon
                            checkedIcon={<CheckCircleIcon />} // Checked icon
                        />
                        }
                        label=""
                        />
                        <img src={c.product.images[0]} alt='product image'/>
                        <div className="CartPage__products_manage">
                            <h4><a onClick={()=> navigate(`/product/${c.product.id}`)}>{c.product.title}</a></h4>
                            <p>In stock</p>
                            <nav className="CartPage__products_nav">
                                <div className="CartPage__products_nav select">
                                    <p>Qty:</p>
                                    <select id="qty" value={c.quantities} name="qty" onChange={(e) => handleQuantityChange(e, c.id)}>
                                        <option value='1'>1</option>
                                        <option value='2'>2</option>
                                        <option value='3'>3</option>
                                        <option value='4'>4</option>
                                        <option value='5'>5</option>
                                        <option value='6'>6</option>
                                        <option value='7'>7</option>
                                        <option value='8'>8</option>
                                        <option value='9'>9</option>
                                        <option value='10'>10</option>
                                    </select> 
                                </div> |
                                <a onClick={() => deleteitem(c.id)}>Delete</a> 
                            </nav>
                        </div>
                        <div className="CartPage__products_price">
                            <h4>&euro;{c.product.price}</h4>
                        </div>
                    </div> ))}

                    <div className="CartPage__products_total-price">
                        <p>Subtotal  [{cart.length} items]: <strong>&euro;{total}</strong></p>
                        <div className="CartPage__products_total-button">
                            <button onClick={() => Aftercheckout()}>Proceed to Checkout</button>
                        </div> 
                    </div>
                </div>
            </div>
          </div>
          <PageFooter/>
        </div>
      )
    }
    
export default CartPage