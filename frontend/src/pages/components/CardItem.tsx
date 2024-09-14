import ShoppingCartIcon from '@mui/icons-material/ShoppingCart';
import IconButton from '@mui/material/IconButton';
import { yellow } from '@mui/material/colors';
import { useNavigate } from 'react-router-dom';

import useAppDispatch from '../../hooks/useAppDispatch';
import useAppSelector from '../../hooks/useAppSelector';
import { addItem } from '../../redux/reducers/cartReducer';
import Product from '../../types/Product';

const CardItem = (props: Product) => {
  const {cart} = useAppSelector(state => state.cartReducer);
  const dispatch = useAppDispatch();
  const navigate = useNavigate();
  let CartId:number = cart.length+1;
  const Additem = () => {
    dispatch(addItem(
      {
        id: CartId,
        product: {
          id: props.id,
          title: props.title,
          price: props.price,
          images: props.images,
          category: props.category,
          description: props.description
        },
        quantities: 1
      }
      )) 
    }

  return (
      <div className='CardItem'>
        <div className='CardItem__inside-box'>
          <img src={props.images[0]} alt='product image'/>
          <h4><a onClick={()=> navigate(`/product/${props.id}`)}>{props.title}</a></h4>
          <p>&euro;{props.price}</p>
          <div className='CardItem__cartIcon'>
            <IconButton sx={{ color: yellow[700] }} onClick={() => Additem()}>
              <ShoppingCartIcon/>
            </IconButton>
          </div>
        </div>
      </div>
    )
}

export default CardItem