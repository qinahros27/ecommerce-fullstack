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