import {useState, useEffect} from 'react';
import axios from "axios";
import { Guid } from 'guid-typescript';

import { fetchAProduct } from '../../redux/reducers/productsReducer';
import { updateAProduct } from "../../redux/reducers/productsReducer";
import useAppSelector from '../../hooks/useAppSelector';
import useAppDispatch from '../../hooks/useAppDispatch';
import { fetchAllCategories } from "../../redux/reducers/categoryReducer";

const EditProducts = () => {
    const {categories} = useAppSelector(state => state.categoriesReducer); 
    const {productDetail} = useAppSelector(state => state.productsReducer);
    const dispatch = useAppDispatch();
    const [title,setTitle] = useState('');
    const [description,setDesciption] = useState('');
    const [price, setPrice] = useState(0);
    const [category,setCategory] = useState<Guid>(Guid.createEmpty());
    const [inventory,setInventory] = useState(0);
    const [images, setImages] = useState<string[]>([]);
    const [edited, setEdited] = useState(false);
    const [find,setFind] = useState<Guid>(Guid.createEmpty());

    const EditProduct = () => {
        if(title != '' && description != '' && price != 0 && category!= null && images.length != 0) {
            dispatch(updateAProduct({productData: {title: title, price: price, description: description, categoryId:category, images: images, inventory: inventory}, productId: productDetail.id as Guid}));
            setEdited(true);
        }
        else {
            alert('There might something wrong');
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

    const handleImageAdd = (event: React.ChangeEvent<HTMLInputElement>) => {
        const file = event.target.files?.[0]; // Get the selected file
        if (file) {
          const formData = new FormData();
          formData.append('file', file); // Append the file to the FormData
          AddImage(formData);
        }
    };
      
    const AddImage = (formData: FormData) => {
        axios
          .post('https://api.escuelajs.co/api/v1/files/upload', formData, {
            headers: {
              'Content-Type': 'multipart/form-data',
            },
          })
          .then((res) => {
            setImages((prevImages) => [...prevImages, res.data.location]);
          })
          .catch((err) => console.error(err));
    };

    const handleImageChange = (event: React.ChangeEvent<HTMLInputElement>, currentImage: string) => {
        const file = event.target.files?.[0]; // Get the selected file
        if (file) {
          const formData = new FormData();
          formData.append('file', file); // Append the file to the FormData
          uploadImage(formData, currentImage);
        }
    };
      
    const uploadImage = (formData: FormData, currentImage: string) => {
        axios
          .post('https://api.escuelajs.co/api/v1/files/upload', formData, {
            headers: {
              'Content-Type': 'multipart/form-data',
            },
          })
          .then((res) => {
            setImages((prevImages) => prevImages.map((item) => (item === currentImage ? res.data.location : item)));

          })
          .catch((err) => console.error(err));
    };

    const DoneEdit = () => {
        setEdited(false); 
        setFind(Guid.createEmpty());
        setTitle('');
        setPrice(0);
        setDesciption('');
        setCategory(Guid.createEmpty());
        setImages([]);
    }

    useEffect(() => {
        dispatch(fetchAllCategories());
      },[])

    return (
        <div className="ManageProduct__manage-box_inner">
            <h1>Edit Product</h1>

            <div className="ManageProduct__manage-box_input find-button">
                <h4>Find product: </h4>
                <input value={find.toString()} type='number' name="find" id='find' onChange={(e) => { const parsedValue = parseInt(e.target.value); const guidString = parsedValue.toString() ; setFind(Guid.parse(guidString));}}/>
                <button onClick={() => FindProduct()}>Find</button>
            </div>

            <div className="ManageProduct__manage-box_input">
                <h4>Title: </h4>
                <input type='text' value={title} name="title" id='price' onChange={e => setTitle(e.target.value)}/>
            </div>

            <div className="ManageProduct__manage-box_input">
                <h4>Price: </h4>
                <input type='text' value={price} name="price" id='price' onChange={e => setPrice(parseInt(e.target.value))}/>
            </div>

            <div className="ManageProduct__manage-box_input">
                <h4>Description: </h4>
                <input type='text' value={description} name="description" id='description' onChange={e => setDesciption(e.target.value)}/>
            </div>

            <div className="ManageProduct__manage-box_input">
                <h4>Category: </h4>
                <select id="category" value={category.toString()} name="category" onChange={(e) => { const parsedValue = parseInt(e.target.value); const guidString = parsedValue.toString() ; setCategory(Guid.parse(guidString));}}>
                {categories.map((c) => (
                    <option key={c.id?.toString()} value={c.id?.toString()}>{c.name}</option>
                ))}
                </select>
            </div>


            <div className="ManageProduct__manage-box_input">
                <h4>Images :</h4>
                {images.map((i,index) => ( 
                <div key={index} className="ManageProduct__inner_image-detail edit">
                                <img src={i} alt='product image'/> 
                                <input type="file" id="screenshot" name="screenshot" accept="image/png, image/gif, image/jpeg" onChange={e => handleImageChange(e,i)}/> 
                </div> ))} 
            </div>

            <div className="ManageProduct__manage-box_input">
                <h4>Add picture: </h4>
                <input type="file" id="screenshot" name="screenshot" accept="image/png, image/gif, image/jpeg" onChange={handleImageAdd}/>
            </div>


            <div className="ManageProduct__inner_button">
                <button onClick={() => EditProduct()}>Edit</button>
            </div>

            {edited && 
            <div className="ManageProduct__inner_added-detail">
                <h3>Product edited. Here is the product detail: </h3>
                <p><strong>Id: </strong>{productDetail.id?.toString()}</p>
                <p><strong>Title: </strong>{productDetail.title}</p>
                <p><strong>Price: </strong>{productDetail.price}</p>
                <p><strong>Description: </strong>{productDetail.description}</p>
                <p><strong>Category: </strong>{productDetail.category.name}</p>
                <p><strong>Images of product: </strong></p>

                <div className="ManageProduct__inner_image-detail">
                    {productDetail.images.map((i,index) => (  
                            <img src={i} alt='product image'/>  
                    ))}
                </div>

                <div className="ManageProduct__inner_button">
                    <button onClick={() => DoneEdit()}>Done</button>
                </div>
            </div> }            
        </div>
      )
}
    
export default EditProducts