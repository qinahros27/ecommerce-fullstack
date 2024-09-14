import {useState} from 'react';
import Dialog from '@mui/material/Dialog';
import DialogActions from '@mui/material/DialogActions';
import DialogContent from '@mui/material/DialogContent';
import DialogContentText from '@mui/material/DialogContentText';
import Button from '@mui/material/Button';
import {  parse } from 'uuid';
import { v4 as uuidv4 } from 'uuid';

import { fetchAProduct } from '../../redux/reducers/productsReducer';
import { deleteAProduct } from "../../redux/reducers/productsReducer";
import useAppSelector from '../../hooks/useAppSelector';
import useAppDispatch from '../../hooks/useAppDispatch';
import { Guid } from 'guid-typescript';



const DeleteProducts = () => { 
    const {productDetail} = useAppSelector(state => state.productsReducer);
    const dispatch = useAppDispatch();
    const [title,setTitle] = useState('');
    const [description,setDesciption] = useState('');
    const [price, setPrice] = useState(0);
    const [category,setCategory] = useState<Guid>(Guid.createEmpty());
    const [images, setImages] = useState<string[]>([]);
    const [deleted, setDeleted] = useState(false);
    const [find, setFind] = useState<Guid>(Guid.createEmpty());
    const [dialog,setDialog] = useState(false);

    const DeleteProduct = () => {
        if(title != '' && description != '' && price != 0 && category != null && images.length != 0) {
            dispatch(deleteAProduct({productId: productDetail.id as Guid}));
            setDeleted(true);
        }
        else {
            alert('There is no product to delete.');
        }
    }

    const FindProduct = () => {
        if(find != null) {
            dispatch(fetchAProduct({productId: find}));
                setTitle(productDetail.title);
                setPrice(productDetail.price);
                setDesciption(productDetail.description);
                setCategory(productDetail.category.id as Guid);
                setImages(productDetail.images);    
        }
        else {
            alert('Please enter the id of the product');
        }
    }

    const handleClickOpen = () => {
        setDialog(true);
    };
    
      const handleClose = () => {
        setDialog(false);
    };

    const DoneDelete = () => {
        setDeleted(false); 
        setFind(Guid.createEmpty());
        setTitle('');
        setPrice(0);
        setDesciption('');
        setCategory(Guid.createEmpty());
        setImages([]);
    }

    return (
        <div className="ManageProduct__manage-box_inner">
                        <h1>Delete Product</h1>

                        <div className="ManageProduct__manage-box_input find-button">
                            <h4>Find product: </h4>
                            <input value={find.toString()} type='number' name="find" id='find' onChange={(e) => { const parsedValue = parseInt(e.target.value); const guidString = parsedValue.toString() ; setFind(Guid.parse(guidString));}}/>
                            <button onClick={() => FindProduct()}>Find</button>
                        </div>

                        <div className="ManageProduct__manage-box_input">
                            <h4>Title: </h4>
                            <p>{title}</p>
                        </div>

                        <div className="ManageProduct__manage-box_input">
                            <h4>Price: </h4>
                            <p>{price}</p>
                        </div>

                        <div className="ManageProduct__manage-box_input">
                            <h4>Description: </h4>
                            <p>{description}</p>
                        </div>

                        <div className="ManageProduct__manage-box_input">
                            <h4>Category: </h4>
                            <p>{category.toString()}</p>
                        </div>

                        <div className="ManageProduct__manage-box_input">
                            <h4>Images :</h4>
                            {images.map((i,index) => ( 
                            <div key={index} className="ManageProduct__inner_image-detail edit">
                                <img src={i} alt='product image'/> 
                            </div> ))} 
                        </div>
                        
                        <div className="ManageProduct__inner_button">
                            <button onClick={handleClickOpen}>Delete</button>
                        </div>

                        <Dialog
                            open={dialog}
                            onClose={handleClose}
                            aria-labelledby="alert-dialog-title"
                            aria-describedby="alert-dialog-description"
                        >
                            <DialogContent>
                            <DialogContentText id="alert-dialog-description">
                                Are you sure you want to delete this item? 
                            </DialogContentText>
                            </DialogContent>
                            <DialogActions>
                            <Button color='error' onClick={() => {handleClose(); DeleteProduct()}}>Delete</Button>
                            <Button color='error' onClick={handleClose} autoFocus>
                                Cancel
                            </Button>
                            </DialogActions>
                        </Dialog>

                        {deleted && 
                        <div className="ManageProduct__inner_delete">
                            <h4>Result: Item Deleted</h4>
                            <div className="ManageProduct__inner_button">
                                <button onClick={() => DoneDelete()}>Done</button>
                            </div>
                        </div> }
                    </div>
      )
}
    
export default DeleteProducts