import {useState, useEffect} from 'react';
import axios from "axios";
import { Guid } from 'guid-typescript';

import { createAProduct} from "../../redux/reducers/productsReducer";
import useAppSelector from '../../hooks/useAppSelector';
import useAppDispatch from '../../hooks/useAppDispatch';
import { fetchAllCategories } from "../../redux/reducers/categoryReducer";
import withAuth from '../../authenticate/authenticateCheck';

const AddProducts = () => {
    const {categories} = useAppSelector(state => state.categoriesReducer); 
    const {productDetail} = useAppSelector(state => state.productsReducer);
    const dispatch = useAppDispatch();
    const [title,setTitle] = useState('');
    const [description,setDesciption] = useState('');
    const [price, setPrice] = useState(0);
    const [category,setCategory] = useState<Guid>(Guid.createEmpty());;
    const [inventory, setInventory] = useState(0);
    const [images, setImages] = useState<string[]>([]);
    const [added, setAdded] = useState(false);

    const AddProduct = () => {
        if(title != '' && description != '' && price != 0 && category!= null && images.length != 0&& inventory != 0) {
            dispatch(createAProduct({productData: {title: title, price: price, description: description,inventory: inventory, categoryId:category, images: images}}));
            setAdded(true);
        }
        else {
            alert('Please enter all the input');
        }
    }

    const handleImageChange = (event: React.ChangeEvent<HTMLInputElement>) => {
        const file = event.target.files?.[0]; // Get the selected file
        if (file) {
          const formData = new FormData();
          formData.append('file', file); // Append the file to the FormData
          uploadImage(formData);
        }
      };
      
    const uploadImage = (formData: FormData) => {
        axios
          .post('https://api.escuelajs.co/api/v1/files/upload', formData, {
            headers: {
              'Content-Type': 'multipart/form-data',
            },
          })
          .then((res) => {
            console.log(res.data.location);
            setImages((prevImages) => [...prevImages, res.data.location]);
          })
          .catch((err) => console.error(err));
    };

    useEffect(() => {
        dispatch(fetchAllCategories());
      },[])

    return (
        <div className="ManageProduct__manage-box_inner">
            <h1>Add Product</h1>

            <div className="ManageProduct__manage-box_input">
                <h4>Title: </h4>
                <input type='text' name="title" id='price' onChange={e => setTitle(e.target.value)}/>
            </div>

            <div className="ManageProduct__manage-box_input">
                <h4>Price: </h4>
                <input type='text' name="price" id='price' onChange={e => setPrice(parseInt(e.target.value))}/>
            </div>

            <div className="ManageProduct__manage-box_input">
                <h4>Description: </h4>
                <input type='text' name="description" id='description' onChange={e => setDesciption(e.target.value)}/>
            </div>

            <div className="ManageProduct__manage-box_input">
                <h4>Inventory: </h4>
                <input type='text' name="inventory" id='inventory' onChange={e => setInventory(parseInt(e.target.value))}/>
            </div>

            <div className="ManageProduct__manage-box_input">
                <h4>Category: </h4>
                <select id="category" name="category" onChange={(e) => { const parsedValue = parseInt(e.target.value); const guidString = parsedValue.toString() ; setCategory(Guid.parse(guidString));}}>
                {categories.map((c) => (
                    <option key={c.id?.toString()} value={c.id?.toString()}>{c.name}</option>
                ))}
                </select>
            </div>

            <div className="ManageProduct__manage-box_image">
                <h4>Choose at least 3 images for products: </h4>

                <div className="ManageProduct__manage-box_image-upload">
                    <p>First picture: </p>
                    <input type="file" id="screenshot" name="screenshot" accept="image/png, image/gif, image/jpeg" onChange={handleImageChange}/>
                </div>

                <div className="ManageProduct__manage-box_image-upload">
                    <p>Second picture: </p>
                    <input type="file" id="screenshot" name="screenshot" accept="image/png, image/gif, image/jpeg" onChange={handleImageChange}/>
                </div> 

                <div className="ManageProduct__manage-box_image-upload">
                    <p>Third picture: </p>
                    <input type="file" id="screenshot" name="screenshot" accept="image/png, image/gif, image/jpeg" onChange={handleImageChange}/>
                </div>     
            </div>

            <div className="ManageProduct__inner_button">
                <button onClick={() => AddProduct()}>Add</button>
            </div>

            {added && 
            <div className="ManageProduct__inner_added-detail">
                <h3>Product added. Here is the product detail: </h3>
                <p><strong>Title: </strong>{productDetail.title}</p>
                <p><strong>Price: </strong>{productDetail.price}</p>
                <p><strong>Description: </strong>{productDetail.description}</p>
                <p><strong>Category: </strong>{productDetail.category.name}</p>
                <p><strong>Images of product: </strong></p>

                <div className="ManageProduct__inner_image-detail">
                    {productDetail.images.map((i,index) => (  
                            <img key={index} src={i} alt='product image'/>   
                    ))}
                </div>
            </div> }              
        </div>
      )
}
    
export default withAuth(AddProducts);