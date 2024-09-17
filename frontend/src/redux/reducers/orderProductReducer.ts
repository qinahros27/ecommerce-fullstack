import { createAsyncThunk,createSlice } from "@reduxjs/toolkit";

import { Guid } from "guid-typescript";
import axios, { AxiosError } from "axios";
import {OrderCreate, OrderRead, UserOrdersRead }from "../../types/Order";

const initialState: {
    orderCreate: OrderCreate,
    order: OrderRead,
    orders: OrderRead[],
    userOrder: UserOrdersRead[],
    loading: boolean,
    error: string
} = {
    orderCreate: {
        address: '',
        postCode: '',
        city: '',
        country: '',
        phoneNumber: '',
        userId: Guid.createEmpty()
    },
    order: {
        id: Guid.createEmpty(),
        postCode: '',
        city: '',
        country: '',
        phoneNumber: '',
        userId: Guid.createEmpty(),
    },
    orders : [],
    userOrder: [],
    loading: false,
    error: ""
}


export const fetchAllOrdersByUserId = createAsyncThunk(
    'fetchAllOrdersByUserId',
    async ({ userId}: { userId: Guid }) => {
        try {
            const result = await axios.get<OrderRead[]>(`http://localhost:5102/api/v1/orders/userId/${userId}`);
            return result.data; 
          } catch (e) {
            const error = e as AxiosError;
            return error;
          }
    }
);

const ordersSlice = createSlice({
    name: "orders",
    initialState,
    reducers: {
      cleanUpOrderReducer: () => {
        return initialState
      }
    } ,
    extraReducers: (build) => {
        build
        .addCase(fetchAllOrdersByUserId.pending, (state, action) => {
            state.loading = true
        })
        .addCase(fetchAllOrdersByUserId.rejected, (state, action) => {
            state.error = "Cannot fetch data"
        })
        .addCase(fetchAllOrdersByUserId.fulfilled, (state, action) => {
            if (action.payload instanceof AxiosError) {
                state.error = action.payload.message
            } else {
                state.orders = action.payload;
                
            }
            state.loading = false
        })
    }
})

const ordersReducer = ordersSlice.reducer
export const { cleanUpOrderReducer } = ordersSlice.actions
export default ordersReducer