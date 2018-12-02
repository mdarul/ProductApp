class App extends React.Component {
    constructor(props) {
        super(props);

        this.state = {
            showOneActive: false,
            addActive: false,
            updateActive: false,
            showAllActive: false
        };

        this.handleShowProductClick = this.handleShowProductClick.bind(this);
        this.handleAddProductClick = this.handleAddProductClick.bind(this);
        this.handleShowAllProductsClick = this.handleShowAllProductsClick.bind(this);
        this.handleUpdateProductClick = this.handleUpdateProductClick.bind(this);
    }

    handleShowProductClick() {
        this.setState(
            {
                showOneActive: true,
                addActive: false,
                updateActive: false,
                showAllActive: false
            }
        );
    }

    handleAddProductClick() {
        this.setState(
            {
                showOneActive: false,
                addActive: true,
                updateActive: false,
                showAllActive: false
            }
        );
    }

    handleShowAllProductsClick() {
        this.setState(
            {
                showOneActive: false,
                addActive: false,
                updateActive: false,
                showAllActive: true
            }
        );
    }

    handleUpdateProductClick() {
        this.setState(
            {
                showOneActive: false,
                addActive: false,
                updateActive: true,
                showAllActive: false
            }
        );
    }

    render() {
        return (
            <div className="grid">
                <SideBar
                    handleShowProductClick={this.handleShowProductClick}
                    handleAddProductClick={this.handleAddProductClick}
                    handleShowAllProductsClick={this.handleShowAllProductsClick}
                    handleUpdateProductClick={this.handleUpdateProductClick} />

                <div className="mainFrame">
                    <div id="mainFrameContent">
                        {this.state.showOneActive === true ? <ShowProductFrame /> : ""}
                        {this.state.addActive === true ? <AddProductFrame /> : ""}
                        {this.state.updateActive === true ? <UpdateProductFrame /> : ""}
                        {this.state.showAllActive === true ? <ShowAllProductsFrame /> : ""}                            
                    </div>
                </div>
            </div>
        );
    }
}

class SideBar extends React.Component {
    constructor(props) {
        super(props);

        this.state = {
            handleShowProductClick: props.handleShowProductClick,
            handleAddProductClick: props.handleAddProductClick,
            handleShowAllProductsClick: props.handleShowAllProductsClick,
            handleUpdateProductClick: props.handleUpdateProductClick
        };
    }

    render() {
        return (
            <div className="sidebar">
                <div id="sidebarContent">
                    <div className="sidebarEntity" onClick={this.state.handleShowProductClick}>Show product</div>
                    <div className="sidebarEntity" onClick={this.state.handleAddProductClick}>Add product</div>
                    <div className="sidebarEntity" onClick={this.state.handleUpdateProductClick}>Update product</div>
                    <div className="sidebarEntity" onClick={this.state.handleShowAllProductsClick}>Show all products</div>
                </div>
            </div>
        );
    }
}

class ShowProductFrame extends React.Component {
    constructor(props) {
        super(props);

        this.state = {
            product: null
        }

        this.handleProductGet = this.handleProductGet.bind(this);
    }

    handleProductGet(newProduct) {
        this.setState({
            product: newProduct
        });
    }

    render() {
        return (
            <div>
                {this.state.product === null ? <GetProductForm handleProductGet={this.handleProductGet} /> : <ShowProductEntity product={this.state.product} />}
            </div>
        );
    }
}

class GetProductForm extends React.Component {
    constructor(props) {
        super(props);

        this.state = {
            handleProductGet: props.handleProductGet,
            product: null,
            key: ""
        }

        this.handleSubmit = this.handleSubmit.bind(this);
        this.handleStateChange = this.handleStateChange.bind(this);
    }

    handleStateChange(event) {
        this.setState({
            [event.target.name]: event.target.value
        });
    }

    handleSubmit(event) {
        const http = new XMLHttpRequest();
        const url = "https://" + location.host + "/api/products/" + this.state.key;

        http.open("GET", url);
        http.send();
        http.onreadystatechange = (e) => {
            console.log(http.responseText);
            if (http.status === 200 && http.responseText !== "") {
                this.state.handleProductGet(http.responseText);
            }
        };

        event.preventDefault();
    }

    render() {
        return (
            <form onSubmit={this.handleSubmit} className="openSansFont">
                <label>
                    Product ID:
                    <br />
                    <input
                        name="key"
                        type="text"
                        value={this.state.key}
                        onChange={this.handleStateChange}
                    />
                </label>
                <input type="submit" value="Search" className="openSansFont"/>
            </form>
        );
    }
}

class ShowProductEntity extends React.Component {
    constructor(props) {
        super(props);

        this.state = {
            product: JSON.parse(props.product)
        }
    }

    render() {
        return (
            <div className="productEntity openSansFont">
                Id: {this.state.product.id}
                <br />
                Name: {this.state.product.name}
                <br />
                Category: {this.state.product.category}
                <br />
                Cost: {this.state.product.cost}
            </div>
        );
    }
}

class AddProductFrame extends React.Component {
    constructor(props) {
        super(props);

        this.state = {
            name: "",
            category: "",
            cost: 0.0
        }

        this.handleSubmit = this.handleSubmit.bind(this);
        this.handleStateChange = this.handleStateChange.bind(this);
    }

    handleStateChange(event) {
        this.setState({
            [event.target.name]: event.target.value
        });
    }

    handleSubmit(event) {
        if (this.state.name === "") {
            alert("Please enter a product name");
            return;
        }
        else if (this.state.category === "") {
            alert("Please enter a product category");
            return;
        }
        else {
            const http = new XMLHttpRequest();
            const url = "https://" + location.host + "/api/products/";

            http.open("POST", url);
            http.setRequestHeader("Content-type", "application/json");
            http.send(JSON.stringify({
                name: this.state.name,
                category: this.state.category,
                cost: this.state.cost
            }));
            http.onreadystatechange = (e) => {
                console.log(http.responseText);
                if (http.status === 204) {
                    alert("Product added successfully");
                }
            };

            event.preventDefault();
        }
    }

    render() {
        return (
            <div>
                <form onSubmit={this.handleSubmit} className="openSansFont">
                    <label>
                        Product name:
                        <br />
                        <input
                            name="name"
                            type="text"
                            value={this.state.name}
                            onChange={this.handleStateChange}
                        />
                    </label>
                    <label>
                        Product category:
                        <br />
                        <input
                            name="category"
                            type="text"
                            value={this.state.category}
                            onChange={this.handleStateChange}
                        />
                    </label>
                    <label>
                        Product cost:
                        <br />
                        <input
                            name="cost"
                            type="number"
                            value={this.state.cost}
                            onChange={this.handleStateChange}
                        />
                    </label>
                    <input type="submit" value="Add product" className="openSansFont"/>
                </form>
            </div>
        );
    }
}

class UpdateProductFrame extends React.Component {
    constructor(props) {
        super(props);

        this.state = {
            id: "",
            name: "",
            category: "",
            cost: 0.0
        }

        this.handleSubmit = this.handleSubmit.bind(this);
        this.handleStateChange = this.handleStateChange.bind(this);
    }

    handleStateChange(event) {
        this.setState({
            [event.target.name]: event.target.value
        });
    }

    handleSubmit(event) {
        if (this.state.id === "") {
            alert("Please enter a product ID");
            return;
        }
        else if (this.state.name === "") {
            alert("Please enter a product name");
            return;
        }
        else if (this.state.category === "") {
            alert("Please enter a product category");
            return;
        }
        else {
            const http = new XMLHttpRequest();
            const url = "https://" + location.host + "/api/products/" + this.state.id;

            http.open("PUT", url);
            http.setRequestHeader("Content-type", "application/json");
            http.send(JSON.stringify({
                name: this.state.name,
                category: this.state.category,
                cost: this.state.cost
            }));
            http.onreadystatechange = (e) => {
                console.log(http.responseText);
                if (http.status === 200) {
                    alert("Product updated successfully");
                }
            };

            event.preventDefault();
        }
    }

    render() {
        return (
            <div>
                <form onSubmit={this.handleSubmit} className="openSansFont">
                    <label>
                        Product ID:
                        <br />
                        <input
                            name="id"
                            type="text"
                            value={this.state.id}
                            onChange={this.handleStateChange}
                        />
                    </label>
                    <label>
                        Product name:
                        <br />
                        <input
                            name="name"
                            type="text"
                            value={this.state.name}
                            onChange={this.handleStateChange}
                        />
                    </label>
                    <label>
                        Product category:
                        <br />
                        <input
                            name="category"
                            type="text"
                            value={this.state.category}
                            onChange={this.handleStateChange}
                        />
                    </label>
                    <label>
                        Product cost:
                        <br />
                        <input
                            name="cost"
                            type="number"
                            value={this.state.cost}
                            onChange={this.handleStateChange}
                        />
                    </label>
                    <input type="submit" value="Update product" />
                </form>
            </div>
        );
    }
}

class ShowAllProductsFrame extends React.Component {
    constructor(props) {
        super(props);

        this.state = {
            products: null
        }

        this.getAllProducts();
    }

    getAllProducts() {
        const http = new XMLHttpRequest();
        const url = "https://" + location.host + "/api/products/";

        http.open("GET", url);
        http.send();
        http.onreadystatechange = () => {
            if (http.status === 200 && http.responseText !== "") {
                console.log(http.responseText);
                this.setState({
                    products: http.responseText
                });
            }
        };
    }

    render() {
        let renderedElements;
        if (this.state.products != null) {
            renderedElements = JSON.parse(this.state.products).map(item => <ShowProductEntity key={item.id} product={JSON.stringify(item)} />);
        }

        return (
            <div>
                {renderedElements}
            </div>
        );
    }
}

ReactDOM.render(<App />, document.getElementById('content'));