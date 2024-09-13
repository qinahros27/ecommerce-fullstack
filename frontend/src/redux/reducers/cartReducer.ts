import { PayloadAction,  createSlice } from "@reduxjs/toolkit";

import Cart from "../../types/Cart";
import CartUpdateQty from "../../types/CartUpdateQty";

const initialState: {
    cart: Cart[] 
} = {
    cart: []
}

const cartSlice = createSlice({
    name: "cart",
    initialState,
    reducers: {
        addItem: (state, action: PayloadAction<Cart>) => {
            const existingItem = state.cart.findIndex(c => c.product.id === action.payload.product.id);
            if (existingItem != -1) {
                state.cart[existingItem].quantities = state.cart[existingItem].quantities + action.payload.quantities;
            } else {
                state.cart.push(action.payload);
            }
        },
        emptyCartReducer: (state) => {
            state.cart = []
        },
        updateQuantity: (state, action: PayloadAction<CartUpdateQty>) => {
            state.cart = state.cart.map((c) => {
              if (c.id === action.payload.id) {
                c.quantities = action.payload.quantities;
              }
              return c;
            });
        },
        deleteItem: (state,action: PayloadAction<Cart>) => {
            state.cart = state.cart.filter(c => c.id !== action.payload.id);
        }
    }
})

const cartReducer = cartSlice.reducer
export const
    {
        addItem,
        emptyCartReducer,
        updateQuantity,
        deleteItem
    } = cartSlice.actions
export default cartReducer 