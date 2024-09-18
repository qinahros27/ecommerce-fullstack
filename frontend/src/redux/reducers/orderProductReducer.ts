import { createAsyncThunk,createSlice } from "@reduxjs/toolkit";

import { Guid } from "guid-typescript";
import axios, { AxiosError } from "axios";
import { OrderProductCreate, OrderOfOrderProductRead, OrderProductRead } from "../../types/OrderProduct";

const initialState: {
    orderProductCreate: OrderProductCreate,
    orderProduct: OrderProductRead,
    ordersProducts: OrderProductRead[],
    orderOfProducts: OrderOfOrderProductRead[],
    loading: boolean,
    error: string
} = {
    orderProductCreate: {
        orderId: Guid.createEmpty(),
        productId: Guid.createEmpty(),
        quantity: 0
    },
    orderProduct: {
        id: Guid.createEmpty(),
        orderId: Guid.createEmpty(),
        productId: Guid.createEmpty(),
        quantity: 0
    },
    ordersProducts : [],
    orderOfProducts: [],
    loading: false,
    error: ""
}

export const fetchAllOrdersProductsByOrderId = createAsyncThunk(
    'fetchAllOrdersProductsByOrderId',
    async ({ orderId }: { orderId: Guid }) => {
        try {
            const result = await axios.get<OrderOfOrderProductRead[]>(`http://localhost:5102/api/v1/ordersproducts/orderId/${orderId}`);
            return result.data; 
          } catch (e) {
            const error = e as AxiosError;
            return error;
          }
    }
);

export const createAnOrderProduct = createAsyncThunk(
    'createAnOrderProduct',
    async ({orderProductData}: { orderProductData:  OrderProductCreate}) => {
      try {
        const result = await axios.post<OrderProductRead>('http://localhost:5102/api/v1/ordersproducts/', orderProductData);
        return result.data; 
      } catch (e) {
        const error = e as AxiosError;
        return error;
      }
    }
);


const ordersProductsSlice = createSlice({
    name: "ordersProducts",
    initialState,
    reducers: {
      cleanUpOrderProductReducer: () => {
        return initialState
      }
    } ,
    extraReducers: (build) => {
        build
        .addCase(fetchAllOrdersProductsByOrderId.pending, (state, action) => {
            state.loading = true
        })
        .addCase(fetchAllOrdersProductsByOrderId.rejected, (state, action) => {
            state.error = "Cannot fetch data"
        })
        .addCase(fetchAllOrdersProductsByOrderId.fulfilled, (state, action) => {
            if (action.payload instanceof AxiosError) {
                state.error = action.payload.message
            } else {
                state.orderOfProducts = action.payload;
                
            }
            state.loading = false
        })
        .addCase(createAnOrderProduct.fulfilled, (state, action) => {
            if (action.payload instanceof AxiosError) {
                state.error = action.payload.message
            } else {
               state.orderProduct = action.payload;
            }
            state.loading = false
        })
        .addCase(createAnOrderProduct.pending, (state, action) => {
            state.loading = true
        })
        .addCase(createAnOrderProduct.rejected, (state, action) => {
            state.error = "Cannot fetch data"
        })
    }
})

const ordersProductsReducer = ordersProductsSlice.reducer
export const { cleanUpOrderProductReducer } = ordersProductsSlice.actions
export default ordersProductsReducer

