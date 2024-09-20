import { createAsyncThunk,createSlice } from "@reduxjs/toolkit";

import { Guid } from "guid-typescript";
import axios, { AxiosError } from "axios";
import { ShipmentCreate, ShipmentRead, OrderProductShipmentRead} from "../../types/Shipment";

const initialState: {
    shipmentCreate: ShipmentCreate,
    shipment: ShipmentRead,
    shipments: ShipmentRead[],
    productShipment: OrderProductShipmentRead[],
    loading: boolean,
    error: string
} = {
    shipmentCreate: {
        companyShipmentName: '',
        shipmentTrackingNumber: '',
        shipmentState: 'Delivering',
        orderProductId: Guid.createEmpty()
    },
    shipment: {
        id: Guid.createEmpty(),
        companyShipmentName: '',
        shipmentTrackingNumber: '',
        shipmentState: 'Delivering',
        orderProductId: Guid.createEmpty(),
    },
    shipments : [],
    productShipment: [],
    loading: false,
    error: ""
}


export const fetchOrderProductShipment = createAsyncThunk(
    'fetchOrderProductShipment',
    async ({ orderProductId }: { orderProductId: Guid }) => {
        try {
            const result = await axios.get<ShipmentRead[]>(`http://localhost:5102/api/v1/shipments/orderProduct/${orderProductId}`);
            return result.data; 
          } catch (e) {
            const error = e as AxiosError;
            return error;
          }
    }
);

export const createAShipment = createAsyncThunk(
    'createAShipment',
    async ({shipmentData}: { shipmentData: ShipmentCreate }) => {
      try {
        const result = await axios.post<ShipmentRead>('http://localhost:5102/api/v1/shipments/', shipmentData);
        return result.data; 
      } catch (e) {
        const error = e as AxiosError;
        return error;
      }
    }
);

export const updateAShipment = createAsyncThunk(
    'updateAShipment',
    async ({shipmentData, shipmentId}: { shipmentData: ShipmentCreate , shipmentId: Guid}) => {
      try {
        const result = await axios.patch<ShipmentRead>(`http://localhost:5102/api/v1/shipments/${shipmentId}`, shipmentData);
        return result.data; 
      } catch (e) {
        const error = e as AxiosError;
        return error;
      }
    }
);

export const deleteAShipment = createAsyncThunk(
  'deleteAShipment',
  async ({ shipmentId}: { shipmentId: Guid}) => {
    try {
      const result = await axios.delete(`http://localhost:5102/api/v1/shipments/${shipmentId}`);
      return result.data; 
    } catch (e) {
      const error = e as AxiosError;
      return error;
    }
  }
);

const shipmentsSlice = createSlice({
    name: "shipments",
    initialState,
    reducers: {
      cleanUpShipmentReducer: () => {
        return initialState
      }
    } ,
    extraReducers: (build) => {
        build
        .addCase(fetchOrderProductShipment.pending, (state, action) => {
            state.loading = true
        })
        .addCase(fetchOrderProductShipment.rejected, (state, action) => {
            state.error = "Cannot fetch data"
        })
        .addCase(fetchOrderProductShipment.fulfilled, (state, action) => {
            if (action.payload instanceof AxiosError) {
                state.error = action.payload.message
            } else {
                state.productShipment = action.payload;
                
            }
            state.loading = false
        })
        .addCase(createAShipment.fulfilled, (state, action) => {
            if (action.payload instanceof AxiosError) {
                state.error = action.payload.message
            } else {
               state.shipment = action.payload;
            }
            state.loading = false
        })
        .addCase(createAShipment.pending, (state, action) => {
            state.loading = true
        })
        .addCase(createAShipment.rejected, (state, action) => {
            state.error = "Cannot fetch data"
        })
        .addCase(updateAShipment.fulfilled, (state, action) => {
          if (action.payload instanceof AxiosError) {
              state.error = action.payload.message
          } else {
              state.shipment = action.payload;
          }
          state.loading = false
        })
        .addCase(updateAShipment.pending, (state, action) => {
          state.loading = true
        })
        .addCase(updateAShipment.rejected, (state, action) => {
          state.error = "Cannot fetch data"
        })
    }
})

const shipmentsReducer = shipmentsSlice.reducer
export const { cleanUpShipmentReducer } = shipmentsSlice.actions
export default shipmentsReducer