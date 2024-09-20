import { createAsyncThunk,createSlice } from "@reduxjs/toolkit";

import { Guid } from "guid-typescript";
import axios, { AxiosError } from "axios";
import { UserCardCreate, UserCardRead } from "../../types/UserCard";

const initialState: {
    userCardCreate: UserCardCreate,
    userCard: UserCardRead,
    loading: boolean,
    error: string
} = {
    userCardCreate: {
        cardName: '',
        type: '',
        cardNumber: '',
        expiredDate: new Date(),
        cvv: 0,
        userId: Guid.createEmpty()
    },
    userCard: {
        id: Guid.createEmpty(),
        cardName: '',
        type: '',
        cardNumber: '',
        expiredDate: new Date(),
        userId: Guid.createEmpty()
    },
    loading: false,
    error: ""
}

export const createAUserCard = createAsyncThunk(
    'createAUserCard',
    async ({userCardData}: { userCardData: UserCardCreate }) => {
      try {
        const result = await axios.post<UserCardRead>('http://localhost:5102/api/v1/userCards/', userCardData);
        return result.data; 
      } catch (e) {
        const error = e as AxiosError;
        return error;
      }
    }
);

export const updateAUserCard = createAsyncThunk(
    'updateAUserCard',
    async ({userCardData, userCardId}: { userCardData: UserCardCreate , userCardId: Guid}) => {
      try {
        const result = await axios.patch<UserCardRead>(`http://localhost:5102/api/v1/userCards/${userCardId}`, userCardData);
        return result.data; 
      } catch (e) {
        const error = e as AxiosError;
        return error;
      }
    }
);

export const deleteAUserCard = createAsyncThunk(
  'deleteAUserCard',
  async ({ userCardId}: { userCardId: Guid}) => {
    try {
      const result = await axios.delete(`http://localhost:5102/api/v1/userCards/${userCardId}`);
      return result.data; 
    } catch (e) {
      const error = e as AxiosError;
      return error;
    }
  }
);

const userCardsSlice = createSlice({
    name: "userCards",
    initialState,
    reducers: {
      cleanUpUserCardReducer: () => {
        return initialState
      }
    } ,
    extraReducers: (build) => {
        build
        .addCase(createAUserCard.fulfilled, (state, action) => {
            if (action.payload instanceof AxiosError) {
                state.error = action.payload.message
            } else {
               state.userCard = action.payload;
            }
            state.loading = false
        })
        .addCase(createAUserCard.pending, (state, action) => {
            state.loading = true
        })
        .addCase(createAUserCard.rejected, (state, action) => {
            state.error = "Cannot fetch data"
        })
        .addCase(updateAUserCard.fulfilled, (state, action) => {
          if (action.payload instanceof AxiosError) {
              state.error = action.payload.message
          } else {
              state.userCard = action.payload;
          }
          state.loading = false
        })
        .addCase(updateAUserCard.pending, (state, action) => {
          state.loading = true
        })
        .addCase(updateAUserCard.rejected, (state, action) => {
          state.error = "Cannot fetch data"
        })
    }
})

const userCardsReducer = userCardsSlice.reducer
export const { cleanUpUserCardReducer } = userCardsSlice.actions
export default userCardsReducer