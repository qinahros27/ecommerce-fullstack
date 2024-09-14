import { useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';

import useAppDispatch from '../hooks/useAppDispatch';
import useAppSelector from '../hooks/useAppSelector';
import { fetchAllUser, login ,fetchAUser} from '../redux/reducers/userReducer';
import User from '../types/User';
import logo from '../images/websitelogo.png';
import '../styles/style.scss';

const LogIn = () => {
    const [email,setEmail] = useState('');
    const {user} =  useAppSelector(state => state.userReducer);
    const [password, setPassword] = useState('');
    const [message,setMessage] = useState('');
    const navigate = useNavigate();
    const dispatch = useAppDispatch();

    const Signin = async () => {
        if(email != '' && password != '') {
            const response = await dispatch(login({email,password}));
            if(response.meta.requestStatus === 'fulfilled' && user?.firstName!= '') {
                navigate("/");    
            }
            else if ( user?.firstName == '') {
                setMessage('There might be some errors');
            }
            else {
                setMessage('Email or password is incorrect. Please try again');
            }
        }
        else {
            setMessage('Please enter email and password !!'); 
        }
    }

    return (
        <div className='SignUp'>
            <header className='header__img'>
                <img src={logo} alt='website logo'/>
            </header>
            
            <main>
                <div className='SignUp-container'>
                    <h2>Sign In</h2>
                    <div className='SignUp-container__item'>
                        <label htmlFor="email">Email</label>
                        <input type='text' name="email" id='email' onChange={e => setEmail(e.target.value)}/>
                    </div>

                    <div className='SignUp-container__item'>
                        <label htmlFor="password">Password</label>
                        <input type='password' name="password" id='password' onChange={e => setPassword(e.target.value)}/>
                    </div>

                    <p className='SignUp-container__message'>{message}</p>
                    <button onClick={() => Signin()}>Sign In</button>
                    
                    <div className='create-button'>
                        <p>New to Sahara ? </p>
                        <button onClick={() => navigate('/signup')}>Create an account</button>
                    </div>
                </div>
            </main>

            <footer>
                <nav>
                <a href="#">Conditions of Use</a>
                <a href="#">Privacy Notice</a>
                <a href="#">Help</a>
                </nav>
                <p>&#169; 2023, Sahara.com, Inc. or its affiliates</p>
            </footer>
        </div>
    )
}

export default LogIn