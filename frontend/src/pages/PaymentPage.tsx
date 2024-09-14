import HeaderBar from './components/HeaderBar';
import PageFooter from './components/PageFooter';
import AddIcon from '@mui/icons-material/Add';
import { useState } from 'react';
import { useNavigate } from 'react-router-dom';

const PaymentPage = () => {

    const [open,setOpen] = useState(false);
    const [userCard, setUserCard] = useState(false);
    const navigate = useNavigate();
return (
    <div>
      <HeaderBar/>
      <div className='Payment-container'>
        <div className='Payment-container-inside'>
            <div className='Payment-address'>
                <h2>Delivery Address</h2>
                <div className='Payment-address-input'>
                    <p>Street: </p>
                    <input type='text'/>
                </div>
                <div className='Payment-address-input'>
                    <p>City: </p>
                    <input type='text'/>
                </div>
                <div className='Payment-address-input'>
                    <p>Post Code: </p>
                    <input type='text'/>
                </div>
                <div className='Payment-address-input'>
                    <p>Country: </p>
                    <input type='text'/>
                </div>
            </div>

            <div className='Payment-method'>
                <h2>Add a payment method</h2>
                <div className='Payment-method-container'>
                    <div className='Payment-method-box'>
                        <AddIcon/>
                        <p onClick={() => setOpen(!open)}>Add a credit or debit card</p>
                    </div>
                    {open && 
                    <div>
                    { userCard ? 
                    <div className='Card-method-container'>
                        <div className='Card-method-inside'>
                            <div className='Card-method-input'>
                                <p><strong>Name on the card: </strong></p>
                                <div className='Card-method-info'>
                                    <p>NGUYEN HO QUYNH ANH</p>
                                </div>
                            </div>

                            <div className='Card-method-input'>
                                <p><strong>Card number: </strong></p>
                                <div className='Card-method-info'>
                                    <p>4318 7258 6359 3256</p>
                                </div>
                            </div>

                            <div className='Card-method-input'>
                                <p><strong>Expire date: </strong></p>
                                <div className='Card-method-info'>
                                    <p>06/27</p>
                                </div>
                            </div>

                            <div className='Card-method-input'>
                                <p><strong>CVV: </strong> </p>
                                <div className='Card-method-info'>
                                    <p>127</p>
                                </div>
                            </div>

                            <div className='Card-method-input'>
                                <button onClick={() => setUserCard(false)}>Edit</button>
                            </div>    
                        </div>
                    </div>
                    :
                    <div className='Card-method-container'>
                        <div className='Card-method-inside'>
                            <div className='Card-method-input'>
                                <p>Name on the card: </p>
                                <input type='text'/>
                            </div>

                            <div className='Card-method-input'>
                                <p>Card number: </p>
                                <input type='text'/>
                            </div>

                            <div className='Card-method-input'>
                                <p>Expire date: </p>
                                <input type="month"/>
                            </div>

                            <div className='Card-method-input'>
                                <p>CVV: </p>
                                <input type='text'/>
                            </div>

                            <div className='Card-method-input'>
                                <button>Save as default</button>
                            </div>    
                        </div>
                    </div> 
                    }
                    </div>
                    }
                    <div className='Payment-method-box'>
                        <AddIcon/>
                        <p>Pay by PayPal</p>
                    </div>

                    <div className='Payment-method-box'>
                        <AddIcon/>
                        <p>Online payment</p>
                    </div>
                </div>
            </div>

            <div className='Payment-container-inside-button'>
                <button onClick={() => navigate('/order/success')}>Continue payment</button>
            </div>
        </div>
      </div>
      <PageFooter/>
    </div>
  )
}

export default PaymentPage