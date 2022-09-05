import { IInputs, IOutputs } from "./generated/ManifestTypes";
import * as React from 'react';
import { Agent } from "https";
import { error } from "console";
import { isContext } from "vm";
import { AddressResults } from "./AddressResult";
export class SddsAddressLookup implements ComponentFramework.StandardControl<IInputs, IOutputs> {

    private _container: HTMLDivElement;
    private _context: ComponentFramework.Context<IInputs>;
    private _postCodePicklistElement: HTMLSelectElement;
    private inputElement: HTMLInputElement;
    private selectContainer: HTMLDivElement;
    private inputContainer: HTMLDivElement;
    private _value: string;
    private _notifyOutputChanged: () => void;
    public addressDetails: AddressResults;
    private AddressObject = [{}] as [AddressResults];

    private AddressLine: string;
    private SubBuildingName:string;
    private BuildingName:string;
    private BuildingNumber:string;
    private Street:string;
    private Locality:string;
    private Town:string;
    private County:string;
    private Postcode:string;
    private Country:string;
    private XCoordinate:string;
    private YCoordinate:string;
    private UPRN:string;
    // Event listener for changes in the credit card number
    private _postalcodechange: EventListenerOrEventListenerObject;
    /**
     * Empty constructor.
     */
    constructor() {

    }

    /**
     * Used to initialize the control instance. Controls can kick off remote server calls and other initialization actions here.
     * Data-set values are not initialized here, use updateView.
     * @param context The entire property bag available to control via Context Object; It contains values as set up by the customizer mapped to property names defined in the manifest, as well as utility functions.
     * @param notifyOutputChanged A callback method to alert the framework that the control has new outputs ready to be retrieved asynchronously.
     * @param state A piece of data that persists in one session for a single user. Can be set at any point in a controls life cycle by calling 'setControlState' in the Mode interface.
     * @param container If a control is marked control-type='standard', it will receive an empty div element within which it can render its content.
     */
    public init(context: ComponentFramework.Context<IInputs>, notifyOutputChanged: () => void, state: ComponentFramework.Dictionary, container: HTMLDivElement): void {
        // Add control initialization code

        this._container = document.createElement("div");
        this._container.setAttribute("style", "width:100%");
        this._context = context;
        this._notifyOutputChanged = notifyOutputChanged;


        // Add the dropdown control, styling and event listener
       // this._postalcodechange = this.PostalCodeChanged.bind(this);
        this._postCodePicklistElement = document.createElement("select");
        this._postCodePicklistElement.setAttribute("id", "adresslist");
        this._postCodePicklistElement.setAttribute("class", "pcfinputcontrol");
        this._postCodePicklistElement.setAttribute("height", "100px");
        this._postCodePicklistElement.setAttribute("weidth", "400px");
        this._postCodePicklistElement.addEventListener("change", this.Onchange.bind(this));

        var option = document.createElement('option');
        option.text = "No option to select";
        option.value = "";
        this._postCodePicklistElement.appendChild(option);

        this.selectContainer = document.createElement("div");
        this.selectContainer.appendChild(this._postCodePicklistElement);

        this.inputElement = document.createElement("input");
        this.inputElement.setAttribute("type", "text");
        this.inputElement.setAttribute("class", "postcodeinput");
        this.inputElement.setAttribute("placeholder", "Enter postcode here");
        this.inputElement.addEventListener("blur", this.PostalCodeChanged.bind(this));

        this._value =  context.parameters.PostalCode.raw!;
        this.inputElement.setAttribute("value", context.parameters.PostalCode.formatted ? context.parameters.PostalCode.formatted : "");

        this.inputContainer = document.createElement("div");
        this.inputContainer.appendChild(this.inputElement);

        this._container.appendChild(this.inputContainer);
        this._container.appendChild(this.selectContainer);       
        container.appendChild(this._container);


    }

    
    /**
     * This function fetches the address (es) for the post code passed.
     * @returns a JSON object of address details data for the post code.
     */
    private FetchAddressesByPostCode(postCodeValue: string): any {
        var addressJson;
        this.AddressObject.splice(0);
        var parameters = {
            PostCode: postCodeValue //this._context.parameters.PostalCode.raw
        };
        var ths = this;      
        var req = new XMLHttpRequest();
        req.open("POST", "/api/data/v9.2/sdds_AddressLookup", true);
        req.setRequestHeader("OData-MaxVersion", "4.0");
        req.setRequestHeader("OData-Version", "4.0");
        req.setRequestHeader("Accept", "application/json");
        req.setRequestHeader("Content-Type", "application/json; charset=utf-8");
        req.onreadystatechange = function () {
            if (this.readyState === 4) {
                req.onreadystatechange = null;
                if (this.status === 200) {
                    var results = JSON.parse(this.response);
                    addressJson = JSON.parse(results.Response);

                    if(addressJson.results.length > 0){

                        while (ths._postCodePicklistElement.options.length) ths._postCodePicklistElement.remove(0);
                        var dropdown = document.getElementById('adresslist') as HTMLInputElement;

                        var option = document.createElement('option');
                        option.text = "Select address Line";
                        option.value = "";
                        dropdown.appendChild(option);
                        dropdown.value = "";

                        for (let i = 0; i < addressJson.results.length; i++) {
                            var addObj = {} as AddressResults;
                            addObj["AddressLine"] = addressJson.results[i].Address.AddressLine;
                            addObj["BuildingNumber"] = addressJson.results[i].Address.BuildingNumber;
                            addObj["Street"] = addressJson.results[i].Address.Street;
                            addObj["Country"] = addressJson.results[i].Address.Country;
                            addObj["County"] = addressJson.results[i].Address.County;
                            addObj["Locality"] = addressJson.results[i].Address.Locality;
                            addObj["Town"] = addressJson.results[i].Address.Town;
                            addObj["Postcode"] = addressJson.results[i].Address.Postcode;
                            addObj["SubBuildingName"] = addressJson.results[i].Address.SubBuildingName;
                            addObj["XCoordinate"] = addressJson.results[i].Address.XCoordinate;
                            addObj["YCoordinate"] = addressJson.results[i].Address.YCoordinate;
                            addObj["UPRN"] = addressJson.results[i].Address.UPRN;
    
                            ths.AddressObject.push(addObj);
    
                            var option = document.createElement('option');
    
                            option.text = addressJson.results[i].Address.AddressLine;
                            option.value = addressJson.results[i].Address.UPRN;
                            dropdown.appendChild(option);
                        }
                    }                 
                } else {
                    console.log("Error calling the Address API");

                }
            }
        };
        req.send(JSON.stringify(parameters));
        return addressJson;

    }

    private Onchange(): void{
        if(this._postCodePicklistElement.value && this._postCodePicklistElement.value != ""){
            var searchResult = this.AddressObject.find(address => address.UPRN == this._postCodePicklistElement.value);

            if(searchResult){
                this.AddressLine = searchResult.AddressLine;
                this.SubBuildingName = searchResult.SubBuildingName;
                this.BuildingName = searchResult.BuildingName;
                this.BuildingNumber = searchResult.BuildingNumber;
                this.Street = searchResult.Street;
                this.Locality = searchResult.Locality;
                this.Town = searchResult.Town;
                this.Country = searchResult.Country;
                this.County = searchResult.County;
                this.Postcode = searchResult.Postcode;
                this.XCoordinate = searchResult.XCoordinate.toString();
                this.YCoordinate = searchResult.YCoordinate.toString();
                this.UPRN = searchResult.UPRN;
            }

            this._notifyOutputChanged();
        }
    }

    private PostalCodeChanged(): void {

        var poastcodeval = this.inputElement.value;
        if(poastcodeval && poastcodeval.length >= 5){
            
            while (this._postCodePicklistElement.options.length) this._postCodePicklistElement.remove(0);
            this.FetchAddressesByPostCode(poastcodeval);
            this._notifyOutputChanged();
        }
    }

    /**
     * Called when any value in the property bag has changed. This includes field values, data-sets, global values such as container height and width, offline status, control metadata values such as label, visible, etc.
     * @param context The entire property bag available to control via Context Object; It contains values as set up by the customizer mapped to names defined in the manifest, as well as utility functions
     */
     public updateView(context: ComponentFramework.Context<IInputs>): void {       
        // Add code to update control view

        this._value = context.parameters.PostalCode.raw!
        this._context = context;
        this.inputElement.setAttribute("value", context.parameters.PostalCode.formatted ? context.parameters.PostalCode.formatted : "");

        //while (this._postCodePicklistElement.options.length) this._postCodePicklistElement.remove(0);

        //this.FetchAddressesByPostCode();

    }

    /**
     * It is called by the framework prior to a control receiving new data.
     * @returns an object based on nomenclature defined in manifest, expecting object[s] for property marked as “bound” or “output”
     */
    public getOutputs(): IOutputs {

        return {
           postcode: this.Postcode,
           AddressLine1: this.AddressLine,
           subBuildingName: this.SubBuildingName,
           buildingName: this.BuildingName,
           buildingNumber: this.BuildingNumber,
           street: this.Street,
           locality: this.Locality,
           town: this.Town,
           county: this.County,
           country: this.Country,
           xCoordinate: this.XCoordinate,
           yCoordinate: this.YCoordinate,
           uprn: this.UPRN

        };

    }
    /**
     * Called when the control is to be removed from the DOM tree. Controls should use this call for cleanup.
     * i.e. cancelling any pending remote calls, removing listeners, etc.
     */
    public destroy(): void {
        // Add code to cleanup control if necessary
    }


}
