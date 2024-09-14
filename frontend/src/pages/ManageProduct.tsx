import {useState} from 'react';

import AddProducts from "./components/AddProduct";
import EditProducts from "./components/EditProduct";
import DeleteProducts from "./components/DeleteProduct";
import HeaderBar from "./components/HeaderBar";
import PageFooter from "./components/PageFooter";

const ManageProduct = () => {
    const [addState, setaddState] = useState(true);
    const [editState, seteditState] = useState(false);
    const [deleteState, setdeleteState] = useState(false);
    
    return (
        <div>
          <HeaderBar/>
          <div className="ManageProduct">
            <div className="ManageProduct__manage-options">
                <h2>Choose options to manage: </h2>
                <div className={`ManageProduct__manage-options_items ${addState? 'active' : 'inactive'}`} onClick={() => {setaddState(true); seteditState(false); setdeleteState(false)}}>
                    <h3>Add product</h3>
                </div>

                <div className={`ManageProduct__manage-options_items ${editState? 'active' : 'inactive'}`} onClick={() => {setaddState(false); seteditState(true); setdeleteState(false)}}>
                    <h3>Edit product</h3>
                </div>

                <div className={`ManageProduct__manage-options_items ${deleteState? 'active' : 'inactive'}`} onClick={() => {setaddState(false); seteditState(false); setdeleteState(true)}}>
                    <h3>Delete product</h3>
                </div>
            </div>

            <div className="ManageProduct__manage-box">
                {addState && 
                    <AddProducts/>
                }

                {editState && 
                    <EditProducts/>
                }

                {deleteState && 
                    <DeleteProducts/>
                }
            </div>
          </div>
          <PageFooter/>
        </div>
      )
}
    
export default ManageProduct