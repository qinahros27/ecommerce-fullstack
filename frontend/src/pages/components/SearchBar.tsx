import React, { useState } from 'react';
import SearchIcon from '@mui/icons-material/Search';
import CloseIcon from '@mui/icons-material/Close';

import Product from '../../types/Product';
import '../../styles/style.scss';

function SearchBar({ placeholder, data }: {placeholder: string, data: Product[]}) {
    const [filteredData, setFilteredData] = useState<Product[]>();
    const [wordEntered, setWordEntered] = useState("");
  
    const handleFilter = (event: React.ChangeEvent<HTMLInputElement>) => {
      const searchWord = event.target.value;
      setWordEntered(searchWord);
      const newFilter = data.filter((value:Product) => {
        let product = value.title;
        return product.toLowerCase().includes(searchWord.toLowerCase());
      });
      if (searchWord === "") {
        setFilteredData([]);
      } else {
        setFilteredData(newFilter);
      }
    };
  
    const clearInput = () => {
      setFilteredData([]);
      setWordEntered("");
    };
  
    return (
      <div className="SearchBar">
        <div className="SearchBar__input">
          <input type="text" placeholder={placeholder} value={wordEntered} onChange={handleFilter}/>
          <div className="searchIcon">
            {filteredData && filteredData.length === 0 ? (
              <SearchIcon />
            ) : (
              <CloseIcon id="clearBtn" onClick={clearInput} />
            )}
          </div>
        </div>

        {filteredData &&  filteredData.length != 0 && (
          <div className="SearchBar__dataResult">
            {filteredData.slice(0, 5).map((value, key) => {
              return (
                <div className='SearchBar__item'>
                    <a className="SearchBar__dataItem" href={`/product/${value.id}`} >
                        <p>{value.title}</p>
                    </a>
                </div>
              );
            })}
          </div>
        )}
      </div>
    );
}

export default SearchBar