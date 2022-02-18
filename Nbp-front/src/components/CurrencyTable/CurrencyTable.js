import React from 'react';
import styles from './CurrencyTable.module.css';

class CurrencyTable extends React.Component{
  constructor(props){
    super(props);

    this.state = {
      isLoaded: false,
      currencies: [],
      error: "",
      single: null
    }
  }

  componentDidMount(){
    fetch("https://localhost:44391/currencies/all")
      .then(res => res.json())
      .then(
        (result) => {
          this.setState({
            isLoaded: true,
            currencies: result
          });
        },
        (error) => {
          this.setState({
            isLoaded: true,
            error: error
          });
        }
      )
  }

  refreshLine = param => e => {
    fetch("https://localhost:44391/currencies/code/"+param)
    .then(res => res.json())
    .then((result) => {
      this.setState({
        single: result
      });
    })
    .then(() => {
      var index = this.state.currencies.findIndex(element => element.code === param);
      var data = this.state.currencies;
      data[index].mid = this.state.single.mid;
      this.setState({
        currencies: data
      });
    });
  }

  render(){
    const { error, isLoaded, currencies } = this.state;
    if(error){
      return <div>Error: {error.message}</div>
    }
    else if(!isLoaded){
      return <div>Loading...</div>
    }
    else {
      return(
        <div>
          <table className={styles.currencyTable}>
            <thead>
              <tr><th className={styles.columncode}>Code</th><th className={styles.columnname}>Currency</th><th className={styles.columnvalue}>Value</th></tr>
            </thead>
            <tbody>
          {currencies.map((item, index) => (
            <tr key={item.code} className={index %2 === 0 ? styles.even : styles.odd}>
              <td>{item.code}</td>
              <td>{item.currency}</td>
              <td>{item.mid}</td>
              <td><input type="button" value="&#x21bb;" onClick={this.refreshLine(item.code)}/></td>
            </tr>
            ))}
            </tbody>
          </table>
       </div>
      )
    }
  }
}

CurrencyTable.propTypes = {};

CurrencyTable.defaultProps = {};

export default CurrencyTable;
