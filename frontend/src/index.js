import React from 'react';
import ReactDOM from 'react-dom';
import Bootstrap from 'bootstrap';
import BootstrapCSS from 'bootstrap/dist/css/bootstrap.min.css';

class Personlist extends React.Component {
  constructor(props) {
    super(props);

    this.state = {person: []};
  }

  componentDidMount() {
    this.UserList();
  }

  UserList() {
    fetch(`/api/values`)
      .then(result=>result.json())
      .then(items=>this.setState({ person: items }))
  }

  render() {
    const persons = this.state.person.map((item, i) => (
      <div key={i.toString()}>
        <h1>{ item.navn }</h1>
      </div>
    ));

    return (
      <div id="layout-content" className="layout-content-wrapper">
        <div className="panel-list">{ persons }</div>
      </div>
    );
  }
}

class InputForm extends React.Component {
  constructor(props) {
    super(props);
    this.state = {value: ''};

    this.handleChange = this.handleChange.bind(this);
    this.handleSubmit = this.handleSubmit.bind(this);
  }

  handleChange(event) {
    this.setState({value: event.target.value});
  }

  handleSubmit(event) {
    var data = { 'navn': this.state.value };
    fetch(`/api/values`, 
    { method: 'POST',
      body: JSON.stringify(data),
      headers: new Headers({'Content-Type': 'application/json'})
    }).then(() => this.setState({value: ''}));
    event.preventDefault();
  }
  
  render() {
    return (
      <div>
        <form onSubmit={this.handleSubmit}>
          <div className="form-group">
            <label htmlFor="name">Name:</label>
            <input type="text" className="form-control" value={this.state.value} onChange={this.handleChange} />
          </div>
          <button type="submit" className="btn btn-primary">Submit</button>
        </form>
      </div>
    );
  }
}

class Navbar extends React.Component {
  render() {
    return (
      <nav className="navbar navbar-expand-md navbar-dark bg-dark fixed-top">
        <a className="navbar-brand" href="#">Containerized microservice</a>
        <button className="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarsExampleDefault" aria-controls="navbarsExampleDefault" aria-expanded="false" aria-label="Toggle navigation">
          <span className="navbar-toggler-icon"></span>
        </button>

        <div className="collapse navbar-collapse" id="navbarsExampleDefault">
          <ul className="navbar-nav mr-auto">
            <li className="nav-item active">
              <a className="nav-link" href="#">Home <span className="sr-only">(current)</span></a>
            </li>
            <li className="nav-item">
              <a className="nav-link" href="/swagger">API</a>
            </li>
            <li className="nav-item">
              <a className="nav-link" href="/log">Log</a>
            </li>
          </ul>
        </div>
      </nav>
    );
  }
}

ReactDOM.render((
  <div className="container">
    <Navbar />
    <InputForm />
    <Personlist />
  </div>
), document.getElementById('app'))