import { createAsyncThunk, createSlice } from "@reduxjs/toolkit";
import axios, { AxiosError } from "axios";
import { Guid } from "guid-typescript";

import  User from "../../types/User";
import { UserCredential } from "../../types/UserCredential";
import Token from "../../types/Token";

const initialState: {
    user?: User ,
    users: User[],
    checkemail: boolean,
    loading: boolean,
    error: string,
    token: string,
    authenticate: boolean
} = {
    users: [],
    checkemail: false,
    loading: false,
    error: "",
    token: '',
    authenticate: false
}

export const fetchAllUser = createAsyncThunk(
  'fetchAllUser',
  async () => {
    try {
      const result = await axios.get<User[]>('http://localhost:5102/api/v1/users');
      return result.data; // The returned result will be inside action.payload
    } catch (e) {
      const error = e as AxiosError;
      return error;
    }
  }
);

export const fetchAUser = createAsyncThunk(
    'fetchAUser',
    async ({ userId}: { userId: Guid }) => {
      try {
        const result = await axios.get<User>(`http://localhost:5102/api/v1/users/${userId}`);
        return result.data; // The returned result will be inside action.payload
      } catch (e) {
        const error = e as AxiosError;
        return error;
      }
    }
  );

export const createAUser = createAsyncThunk(
    'createAUser',
    async ({userData}: { userData: User }) => {
      try {
        
        const result = await axios.post<User>('http://localhost:5102/api/v1/users', userData);
        return result.data; // The returned result will be inside action.payload
      } catch (e) {
        const error = e as AxiosError;
        return error;
      }
    }
);

export const updateAUser = createAsyncThunk(
    'updateAUser',
    async ({userData, userId}: { userData: User , userId: Guid}) => {
      try {
        const result = await axios.patch<User>(`http://localhost:5102/api/v1/users/${userId}`, userData);
        console.log(result.data)
        return result.data; // The returned result will be inside action.payload
      } catch (e) {
        const error = e as AxiosError;
        return error;
      }
    }
);


export const authenticate = createAsyncThunk(
    "authenticate",
    async (access_token: Token) => {
        try {
            console.log('token',access_token);
            const authentication = await axios.post<User>("http://localhost:5102/api/v1/auth/profile", access_token)
            return authentication.data
        }
        catch (e) {
            const error = e as AxiosError
            return error
        }
    }
)

export const login = createAsyncThunk(
    "login",
    async ({ email, password }: UserCredential, { dispatch }) => {
        try {
            const result = await axios.post<{ access_token: string }>("http://localhost:5102/api/v1/auth", { email, password });
            localStorage.setItem("token", result.data.access_token);
            const authentication = await dispatch(authenticate({token: result.data.toString()}))
            return authentication.payload as User
        }
        catch (e) {
            const error = e as AxiosError
            return error
        }
    }
)

const usersSlice = createSlice({
    name: "users",
    initialState,
    reducers: {
        emptyUserInfo: (state) => {
          state.user = {
            id: Guid.create(),
            email: '',
            password: '',
            firstName: '',
            lastName: '',
            userName: '',
            role: "Customer",
            avatar: ''
          }
        },
        cleanUpUserReducer: () => {
          return initialState
        }
    },
    extraReducers: (build) => {
        build
            .addCase(fetchAllUser.fulfilled, (state, action) => {
              if (action.payload instanceof AxiosError) {
                  state.error = action.payload.message
              } else {
                  state.users = action.payload;    
              }
              state.loading = false
            })
            .addCase(fetchAUser.fulfilled, (state, action) => {
                if (action.payload instanceof AxiosError) {
                    state.error = action.payload.message
                } else {
                    state.user = action.payload;
                    
                }
                state.loading = false
            })
            .addCase(fetchAUser.pending, (state, action) => {
                state.loading = true
            })
            .addCase(fetchAUser.rejected, (state, action) => {
                state.error = "Cannot fetch data"
            })
            .addCase(createAUser.fulfilled, (state, action) => {
                if (action.payload instanceof AxiosError) {
                    state.error = action.payload.message
                } else {
                   state.user = action.payload;
                }
                state.loading = false
            })
            .addCase(createAUser.pending, (state, action) => {
                state.loading = true
            })
            .addCase(createAUser.rejected, (state, action) => {
                state.error = "Cannot fetch data"
            })
            .addCase(updateAUser.fulfilled, (state, action) => {
              if (action.payload instanceof AxiosError) {
                  state.error = action.payload.message
              } else {
                 state.user = action.payload;
              }
              state.loading = false
            })
            .addCase(updateAUser.pending, (state, action) => {
              state.loading = true
            })
            .addCase(updateAUser.rejected, (state, action) => {
              state.error = "Cannot fetch data"
            })
            .addCase(login.fulfilled, (state, action) => {
              if (action.payload instanceof AxiosError) {
                  state.error = action.payload.message
              } else {
                  state.user = action.payload;  
              }
              state.loading = false
            })
            .addCase(authenticate.fulfilled, (state, action) => {
                if (action.payload instanceof AxiosError) {
                    state.error = action.payload.message
                } else {
                    state.user = action.payload
                    console.log(state.user);
                    state.authenticate = true
                }
                state.loading = false
            })
    }
})

const userReducer = usersSlice.reducer
export const
    {
        emptyUserInfo,
        cleanUpUserReducer
    } = usersSlice.actions
export default userReducer