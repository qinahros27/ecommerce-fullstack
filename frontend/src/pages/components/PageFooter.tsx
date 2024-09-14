import appstore from '../../images/appstore.png';
import google from '../../images/google.png';
import logo from '../../images/websitelogo.png';

const PageFooter = () => {
    return (
    <div className="PageFooter">
        <div className="PageFooter__footer">
            <div className="PageFooter__footer_logo">
                <div className="PageFooter__footer_logo-container">
                    <div className="PageFooter__footer_brand-download">
                        <a href="/notfound" target="_blank" rel="noreferrer noopener">
                            <img src={logo} alt="Website logo" />
                        </a>
                    </div>

                    <div className="PageFooter__footer_brand-download">
                        <a href="/notfound" target="_blank" rel="noreferrer noopener">
                            <img src={appstore} alt="Appstore" />
                        </a>
                    </div>
                    
                    <div className="PageFooter__footer_brand-download">
                        <a href="/notfound" target="_blank" rel="noreferrer noopener">
                            <img src={google} alt="Google" />
                        </a>
                    </div>
                </div>
            </div>
            
            <div>
                <div className="PageFooter__nav_title">
                    <p><strong>Get to know us</strong></p>
                </div>
                
                <div>
                    <a href="/notfound" target="_blank" rel="noreferrer noopener"><p>Careers</p><br/></a>
                    <a href="/notfound" target="_blank" rel="noreferrer noopener"><p>Press Releases</p><br/></a>
                    <a href="/notfound" target="_blank" rel="noreferrer noopener"><p>About us</p><br/></a>
                    <a href="/notfound" target="_blank" rel="noreferrer noopener"><p>Blog</p><br/></a>
                    <a href="/notfound" target="_blank" rel="noreferrer noopener"><p>Sahara Science</p></a>
                </div>
            </div>

            <div>
                <div className="PageFooter__nav_title">
                    <p><strong>Make Money with Us</strong></p>
                </div>
                
                <div>
                    <a href="/notfound" target="_blank" rel="noreferrer noopener"><p>Sell on Sahara</p><br/></a>
                    <a href="/notfound" target="_blank" rel="noreferrer noopener"><p>Associates Programme</p><br/></a>
                    <a href="/notfound" target="_blank" rel="noreferrer noopener"><p>Advertise Your Products</p><br/></a>
                    <a href="/notfound" target="_blank" rel="noreferrer noopener"><p>Sahara Pay</p><br/></a>
                    <a href="/notfound" target="_blank" rel="noreferrer noopener"><p>Host an Sahara Hub</p></a>
                </div>
            </div>

            <div>
                <div className="PageFooter__nav_title">
                    <p><strong>Sahara Payment Methods <i className="arrow down"></i></strong></p>
                </div>
                
                <div>
                    <a href="/notfound" target="_blank" rel="noreferrer noopener"><p>Visa Card</p><br/></a>
                    <a href="/notfound" target="_blank" rel="noreferrer noopener"><p>Credit Card</p><br/></a>
                    <a href="/notfound" target="_blank" rel="noreferrer noopener"><p>Gift Cards</p><br/></a>
                    <a href="/notfound" target="_blank" rel="noreferrer noopener"><p>Direct Debit</p><br/></a>
                </div>
            </div>


            <div>
                <div className="PageFooter__nav_title">
                    <p><strong>Let Us Help You <i className="arrow down"></i></strong></p>
                </div>
                
                <div>
                    <a href="/notfound" target="_blank" rel="noreferrer noopener"><p>Track Packages or View Orders</p><br/></a>
                    <a href="/notfound" target="_blank" rel="noreferrer noopener"><p>Returns & Replacements</p><br/></a>
                    <a href="/notfound" target="_blank" rel="noreferrer noopener"><p>Recycling</p><br/></a>
                    <a href="/notfound" target="_blank" rel="noreferrer noopener"><p>Sahara Assistant</p><br/></a>
                    <a href="/notfound" target="_blank" rel="noreferrer noopener"><p>Customer Service</p><br/></a>
                </div>
            </div>

            <div className="PageFooter__nav-bar">
                <nav><a href="/notfound" target="_blank" rel="noreferrer noopener">Accessibility Statement &nbsp;</a></nav>
                <nav><a href="/notfound" target="_blank" rel="noreferrer noopener">Terms and Conditions &nbsp;</a></nav>
                <nav><a href="/notfound" target="_blank" rel="noreferrer noopener">Privacy Policy &nbsp;</a></nav> 
            </div>

            <div className="PageFooter__footer-year">
                <p>&#169; Sahara 2023</p> 
            </div>
        </div> 
     </div>
    )
}
    
export default PageFooter