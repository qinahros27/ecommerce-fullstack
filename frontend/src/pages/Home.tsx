import { useEffect, useState } from 'react';
import { Grid } from '@mui/material';
import Pagination from '@mui/material/Pagination';
import Carousel  from 'react-material-ui-carousel';
import ArrowBackIosNewIcon from '@mui/icons-material/ArrowBackIosNew';
import ArrowForwardIosIcon from '@mui/icons-material/ArrowForwardIos';

import { fetchAllProducts } from '../redux/reducers/productsReducer';
import useAppDispatch from '../hooks/useAppDispatch';
import useAppSelector from '../hooks/useAppSelector';
import HeaderBar from './components/HeaderBar';
import PageFooter from './components/PageFooter';
import CardItem from './components/CardItem';
import SortItem from './components/SortItem';
import mar1 from "../images/mar1.png";
import mar2 from "../images/mar2.png";
import mar3 from "../images/mar3.png";

const Home = () => {
  const [page, setPage] = useState(1);
  const cardsPerPage = 40; // Number of cards to display per page
  const {products} = useAppSelector(state => state.productsReducer);
  const totalPages = Math.ceil(products.length / cardsPerPage);
  const startIndex = (page - 1) * cardsPerPage;
  const endIndex = startIndex + cardsPerPage;
  const currentCards = products.slice(startIndex, endIndex);
  const dispatch = useAppDispatch();

  useEffect(() => {
    dispatch(fetchAllProducts());

  },[])
  
  const handleChangePage = (event: unknown, newPage: number) => {
    setPage(newPage);
  };

return (
    <div>
      <HeaderBar/>
      <div className='Home__marketing'>
        <div className='Home__marketing_carousel'>
            <Carousel animation="slide"  className='Home__marketing_img' NextIcon={<ArrowForwardIosIcon/>} PrevIcon={<ArrowBackIosNewIcon/>}>
              <img src={mar1} alt="marketing picture"/>
              <img src={mar2} alt="marketing picture"/>
              <img src={mar3} alt="marketing picture"/>
            </Carousel>
        </div>
      </div>

      <div className='Home'>
        <div className='Home__item-lists'>
          <div className='Home__item-sort'>
            <h1>Results</h1>
            <SortItem/>
          </div>
          
          <Grid container spacing={2}>
            {currentCards.map((card) => (
              <Grid item xs={12} sm={6} md={3} key={card.id?.toString()}>
                    <CardItem id={card.id} title={card.title} price={card.price} images={card.images} description={card.description} category={card.category}/>
              </Grid>
            ))}
          </Grid>
          <Pagination
            count={totalPages}
            page={page}
            onChange={handleChangePage}
            className='Home__item-pagination'
          />
        </div>
      </div>
      <PageFooter/>
    </div>
  )
}

export default Home