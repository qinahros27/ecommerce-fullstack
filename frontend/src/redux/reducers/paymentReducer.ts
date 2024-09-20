import { createAsyncThunk,createSlice } from "@reduxjs/toolkit";

import { Guid } from "guid-typescript";
import axios, { AxiosError } from "axios";
import { PaymentCreate, PaymentRead } from "../../types/Payment";

const initialState: {
    paymentCreate: PaymentCreate,
    payment: PaymentRead,
    loading: boolean,
    error: string
} = {
    paymentCreate: {
        paymentMethod:'PayPal',
        totalPrice: 0,
        orderId: Guid.createEmpty()
    },
    payment: {
        id: Guid.createEmpty(),
        paymentMethod:'PayPal',
        totalPrice: 0,
        orderId: Guid.createEmpty()
    },
    loading: false,
    error: ""
}

export const createApayment = createAsyncThunk(
    'createApayment',
    async ({paymentData}: { paymentData: PaymentCreate }) => {
      try {
        const result = await axios.post<PaymentRead>('http://localhost:5102/api/v1/payments/', paymentData);
        return result.data; 
      } catch (e) {
        const error = e as AxiosError;
        return error;
      }
    }
);

export const updateApayment = createAsyncThunk(
    'updateApayment',
    async ({paymentData, paymentId}: { paymentData: PaymentCreate , paymentId: Guid}) => {
      try {
        const result = await axios.patch<PaymentRead>(`http://localhost:5102/api/v1/payments/${paymentId}`, paymentData);
        return result.data; 
      } catch (e) {
        const error = e as AxiosError;
        return error;
      }
    }
);

export const deleteAPayment = createAsyncThunk(
  'deleteAPayment',
  async ({ paymentId}: { paymentId: Guid}) => {
    try {
      const result = await axios.delete(`http://localhost:5102/api/v1/payments/${paymentId}`);
      return result.data; 
    } catch (e) {
      const error = e as AxiosError;
      return error;
    }
  }
);

const paymentsSlice = createSlice({
    name: "payments",
    initialState,
    reducers: {
      cleanUpPaymentReducer: () => {
        return initialState
      }
    } ,
    extraReducers: (build) => {
        build
        .addCase(createApayment.fulfilled, (state, action) => {
            if (action.payload instanceof AxiosError) {
                state.error = action.payload.message
            } else {
               state.payment = action.payload;
            }
            state.loading = false
        })
        .addCase(createApayment.pending, (state, action) => {
            state.loading = true
        })
        .addCase(createApayment.rejected, (state, action) => {
            state.error = "Cannot fetch data"
        })
        .addCase(updateApayment.fulfilled, (state, action) => {
          if (action.payload instanceof AxiosError) {
              state.error = action.payload.message
          } else {
              state.payment = action.payload;
          }
          state.loading = false
        })
        .addCase(updateApayment.pending, (state, action) => {
          state.loading = true
        })
        .addCase(updateApayment.rejected, (state, action) => {
          state.error = "Cannot fetch data"
        })
    }
})

const paymentsReducer = paymentsSlice.reducer
export const { cleanUpPaymentReducer } = paymentsSlice.actions
export default paymentsReducer