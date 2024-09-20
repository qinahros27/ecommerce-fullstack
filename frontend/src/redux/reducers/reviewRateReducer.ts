import { createAsyncThunk,createSlice } from "@reduxjs/toolkit";

import { Guid } from "guid-typescript";
import axios, { AxiosError } from "axios";
import { ReviewRateCreate, UserReviewRateRead, ProductReviewRateRead, ReviewRateRead} from "../../types/ReviewRate";

const initialState: {
    reviewRateCreate: ReviewRateCreate,
    userReviewRates: UserReviewRateRead[],
    productReviewRates: ProductReviewRateRead[],
    reviewRateRead: ReviewRateRead,
    loading: boolean,
    error: string
} = {
    reviewRateCreate: {
        userId: Guid.createEmpty(),
        productId: Guid.createEmpty(),
        review: '',
        ratePoint:0
    },
    userReviewRates: [],
    productReviewRates: [],
    reviewRateRead: {
        productId: Guid.createEmpty(),
        review: '',
        ratePoint:0
    },
    loading: false,
    error: ""
}

export const fetchAllReviewRatesByUserId = createAsyncThunk(
    'fetchAllReviewRatesByUserId',
    async ({ userId}: { userId: Guid }) => {
        try {
            const result = await axios.get<UserReviewRateRead[]>(`http://localhost:5102/api/v1/reviewrates/userId/${userId}`);
            return result.data; 
          } catch (e) {
            const error = e as AxiosError;
            return error;
          }
    }
);

export const fetchAllReviewRatesByProductId = createAsyncThunk(
    'fetchAllReviewRatesByProductId',
    async ({ productId}: { productId: Guid }) => {
        try {
            const result = await axios.get<ProductReviewRateRead[]>(`http://localhost:5102/api/v1/reviewrates/productId/${productId}`);
            return result.data; 
          } catch (e) {
            const error = e as AxiosError;
            return error;
          }
    }
);

export const createAReviewRate = createAsyncThunk(
    'createAReviewRate',
    async ({reviewRateData}: { reviewRateData: ReviewRateCreate }) => {
      try {
        const result = await axios.post<ReviewRateRead>('http://localhost:5102/api/v1/reviewrates/', reviewRateData);
        return result.data; 
      } catch (e) {
        const error = e as AxiosError;
        return error;
      }
    }
);

const reviewRatesSlice = createSlice({
    name: "reviewRates",
    initialState,
    reducers: {
      cleanUpReviewRatesReducer: () => {
        return initialState
      }
    } ,
    extraReducers: (build) => {
        build
        .addCase(fetchAllReviewRatesByProductId.pending, (state, action) => {
            state.loading = true
        })
        .addCase(fetchAllReviewRatesByProductId.rejected, (state, action) => {
            state.error = "Cannot fetch data"
        })
        .addCase(fetchAllReviewRatesByProductId.fulfilled, (state, action) => {
            if (action.payload instanceof AxiosError) {
                state.error = action.payload.message
            } else {
                state.productReviewRates = action.payload;
                
            }
            state.loading = false
        })
        .addCase(fetchAllReviewRatesByUserId.pending, (state, action) => {
            state.loading = true
        })
        .addCase(fetchAllReviewRatesByUserId.rejected, (state, action) => {
            state.error = "Cannot fetch data"
        })
        .addCase(fetchAllReviewRatesByUserId.fulfilled, (state, action) => {
            if (action.payload instanceof AxiosError) {
                state.error = action.payload.message
            } else {
                state.userReviewRates = action.payload;
                
            }
            state.loading = false
        })
        .addCase(createAReviewRate.fulfilled, (state, action) => {
            if (action.payload instanceof AxiosError) {
                state.error = action.payload.message
            } else {
               state.reviewRateRead = action.payload;
            }
            state.loading = false
        })
        .addCase(createAReviewRate.pending, (state, action) => {
            state.loading = true
        })
        .addCase(createAReviewRate.rejected, (state, action) => {
            state.error = "Cannot fetch data"
        })
    }
})

const reviewRatesReducer = reviewRatesSlice.reducer
export const { cleanUpReviewRatesReducer } = reviewRatesSlice.actions
export default reviewRatesReducer