import { useEffect, useState} from 'react';
import ShoppingCartIcon from '@mui/icons-material/ShoppingCart';

import logo from '../../images/websitelogo.png';
import '../../styles/style.scss';
import { emptyUserInfo } from '../../redux/reducers/userReducer';
import { fetchAllProducts } from '../../redux/reducers/productsReducer';
import SearchBar from './SearchBar';
import useAppDispatch from '../../hooks/useAppDispatch';
import {useNavigate } from 'react-router-dom';
import useAppSelector from '../../hooks/useAppSelector';

const HeaderBar = () => {
    const {user} = useAppSelector(state => state.userReducer);
    const {products} = useAppSelector(state => state.productsReducer);
    const {cart} = useAppSelector(state => state.cartReducer);
    const dispatch = useAppDispatch();
    const navigate = useNavigate();
    const [open,setOpen] = useState(false);

    const Logout = () => {
      dispatch(emptyUserInfo());
      navigate("/")
    }

    useEffect(() => {
      dispatch(fetchAllProducts());
    },[])

    return (
        <div className='HeaderBar'>
          <img src={logo} alt='website logo' onClick={() => navigate('/')}/>
          <SearchBar placeholder={'Search products'} data={products}/>
          {user && user.firstName != '' && user.lastName != '' ? 
          <div className='HeaderBar__account'>
            <img src={user.avatar} alt='default avatar'/>
            
            <div className='HeaderBar__account_name' onClick={()=>{setOpen(!open)}}>
                <p>{user.firstName} {user.lastName} &nbsp;<span className='HeaderBar__account_icon'>&#9660;</span></p>
            </div> 

            {open &&
                <div className='HeaderBar__account_dropdown'>
                    <ul>
                        <li><a onClick={() => navigate('/account')}>Account Setting</a></li>
                        <li><a href='#'>Help</a></li>
                        <li><a onClick={() => navigate('/your-orders')}>Your order</a></li>
                        <li><a href='#'>Service</a></li>
                        {user.role == 'Admin' &&
                          <li><a onClick={() => navigate('/product/manage')}>Manage Product</a></li>
                        }
                        <div className='HeaderBar__account_dropdown-logout'>
                            <li><a onClick={() => Logout()}>Log out</a></li>
                        </div>
                    </ul>
                </div>
            } 
          </div>
          : 
          <div className='HeaderBar__account' onClick={() => navigate('/login')}>
            <h3>Sign In</h3>
          </div> }

          <div className='HeaderBar__cart' onClick={() => navigate('/cart')}>
            <ShoppingCartIcon/>
            <p><span>{cart.length}</span> item</p>
          </div>
        </div>
    )
}
    
export default HeaderBar