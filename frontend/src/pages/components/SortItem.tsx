import React, { useState, useEffect } from 'react';
import { TextField } from '@mui/material';
import Autocomplete from '@mui/material/Autocomplete';
import InputLabel from '@mui/material/InputLabel';
import MenuItem from '@mui/material/MenuItem';
import FormControl from '@mui/material/FormControl';
import Select, { SelectChangeEvent } from '@mui/material/Select';
import FilterAltIcon from '@mui/icons-material/FilterAlt';
import IconButton from '@mui/material/IconButton';
import { yellow } from '@mui/material/colors';

import Category from '../../types/Category';
import useAppSelector from '../../hooks/useAppSelector';
import useAppDispatch from '../../hooks/useAppDispatch';
import { fetchAllProducts} from '../../redux/reducers/productsReducer';
import { fetchAllCategories } from '../../redux/reducers/categoryReducer';
import '../../styles/style.scss';

const SortItem= () => {
    const [price,setPrice] = useState('');
    const [category, setCategory] = useState<Category | null>(null);
    const [searchValue, setSearchValue] = useState('');
    const {categories} = useAppSelector(state => state.categoriesReducer);
    const dispatch = useAppDispatch();

    const handleChange = (event: SelectChangeEvent) => {
        setPrice(event.target.value);
    };

    const handleSearchChange = (e: React.ChangeEvent<HTMLInputElement>) => {
      setSearchValue(e.target.value);
    };
  
    const handleSelectChange = (e: React.ChangeEvent<{}>, value:Category | null) => {
        setCategory(value);
    };

    const filteredItems = categories.filter((item) =>
      item.name.toLowerCase().includes(searchValue.toLowerCase())
    );

    const returnPrice = (priceString: string) => {
        let priceRange = {min: 0, max: 0};
        switch(priceString) {
            case 'under 100': 
                priceRange.min = 1 ;
                priceRange.max = 100;
                break;
            case '100 to 200':
                priceRange.min = 100;
                priceRange.max= 200;
                break;
            case '200 to 400':
                priceRange.min = 200;
                priceRange.max = 400;
                break;
            case '400 to 600':
                priceRange.min = 400;
                priceRange.max = 600;
                break;
            case '600 to 800':
                priceRange.min = 600;
                priceRange.max = 800;
                break;
            case '800 above':
                priceRange.min = 800;
                priceRange.max = 10000000;
                break;
        }
       return priceRange; 
    } 

    const SortItem = () => {
        let price_range = returnPrice(price);
        if(category === null && price === '') {
            dispatch(fetchAllProducts());
        }   
        else if(category === null) {
            dispatch(fetchAllProducts({min: price_range.min , max: price_range.max}));
        }
        else if(price === '') {
            dispatch(fetchAllProducts({id: category.id}));
        }
        else {
            dispatch(fetchAllProducts({id: category.id , min: price_range.min, max: price_range.max}));
        }
    }

    useEffect(() => {
        dispatch(fetchAllCategories());
      },[])
  
    return (
        <div className='SortItem'>
            <Autocomplete
                options={filteredItems}
                getOptionLabel={(item:any) => item.name}
                value={category}
                onChange={handleSelectChange}
                className='SortItem__Searchbar'
                renderInput={(params:any) => (
                <TextField
                    {...params}
                    label="Category"
                    variant="outlined"
                    onChange={handleSearchChange}
                />
                )}
            />

            <FormControl sx={{ ml: 1,minWidth: 200 }}>
                <InputLabel>Price</InputLabel>
                <Select
                value={price}
                label="Price"
                onChange={handleChange}
                >
                <MenuItem value="">
                    <em>None</em>
                </MenuItem>
                <MenuItem value={'under 100'}>Under &euro;100</MenuItem>
                <MenuItem value={'100 to 200'}>&euro;100 to &euro;200</MenuItem>
                <MenuItem value={'200 to 400'}>&euro;200 to &euro;400</MenuItem>
                <MenuItem value={'400 to 600'}>&euro;400 to &euro;600</MenuItem>
                <MenuItem value={'600 to 800'}>&euro;600 to &euro;800</MenuItem>
                <MenuItem value={'800 above'}>&euro;800 above</MenuItem>
                </Select>
            </FormControl>
            
            <IconButton sx={{ color: yellow[800] }} onClick={() => SortItem()}>
                <FilterAltIcon fontSize='large'/>
            </IconButton>          
        </div> 
    );
  };
  
  export default SortItem;