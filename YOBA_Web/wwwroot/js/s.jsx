// Для вывода отдельного объекта
class Supply extends React.Component {

    constructor(props) {
        super(props);
        this.state = { data: props.supplier };
        this.onClick = this.onClick.bind(this);
    }
    onClick(e) {
        this.props.onRemove(this.state.data);
    }
    render() {
        return <div>
            <p><b>{this.state.data.name}</b></p>
            <p><button onClick={this.onClick}>Delete</button></p>
        </div>;
    }
}

class SupplyForm extends React.Component {

    constructor(props) {
        super(props);
        this.state = { name: "" };

        this.onSubmit = this.onSubmit.bind(this);
        this.onNameChange = this.onNameChange.bind(this);
    }
    onNameChange(e) {
        this.setState({ name: e.target.value });
    }
    onSubmit(e) {
        e.preventDefault();
        var supplyName = this.state.name.trim();
        if (!supplyName) {
            return;
        }
        this.props.onSupplySubmit({ name: supplyName });
        this.setState({ name: "" });
    }
    render() {
        return (
            <form onSubmit={this.onSubmit}>
                <p>
                    <input type="text"
                        placeholder="Supplier name"
                        value={this.state.name}
                        onChange={this.onNameChange} />
                </p>
                <input type="submit" value="Save" />
            </form>
        );
    }
}

// Для вывода всех объектов
class SupplyList extends React.Component {

    constructor(props) {
        super(props);
        this.state = { supplies: [] };

        this.onAddSupply = this.onAddSupply.bind(this);
        this.onRemoveSupply = this.onRemoveSupply.bind(this);
    }
    // загрузка данных
    loadData() {
        var xhr = new XMLHttpRequest();
        xhr.open("get", this.props.apiUrl, true);
        xhr.onload = function () {
            var data = JSON.parse(xhr.responseText);
            this.setState({ supplies: data });
        }.bind(this);
        xhr.send();
    }
    componentDidMount() {
        this.loadData();
    }
    // добавление объекта
    onAddSupply(supplier) {
        if (supplier) {

            const data = new FormData();
            data.append("name", supplier.name);
            var xhr = new XMLHttpRequest();

            xhr.open("post", this.props.apiUrl, true);
            xhr.onload = function () {
                if (xhr.status === 200) {
                    this.loadData();
                }
            }.bind(this);
            xhr.send(data);
        }
    }
    // удаление объекта
    onRemoveSupply(supplier) {

        if (supplier) {
            var url = this.props.apiUrl + "/" + supplier.id;

            var xhr = new XMLHttpRequest();
            xhr.open("delete", url, true);
            xhr.setRequestHeader("Content-Type", "application/json");
            xhr.onload = function () {
                if (xhr.status === 200) {
                    this.loadData();
                }
            }.bind(this);
            xhr.send();
        }
    }
    render() {

        var remove = this.onRemoveSupply;
        return <div>
            <SupplyForm onSupplySubmit={this.onAddSupply} />
            <h2>Supply list</h2>
            <div>
                {
                    this.state.suplies.map(function (supplier) {

                        return <SupplierD key={supplier.Id} supplier={supplier} onRemove={remove} />
                    })
                }
            </div>
        </div>;
    }
}

ReactDOM.render(
    <SupplyList apiUrl="/api/supplies" />,
    document.getElementById("content")
);