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
    private _notifyOutputChanged: () => void;
    public addressDetails: AddressResults;
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

        this._container = container;
        this._context = context;
        this._notifyOutputChanged = notifyOutputChanged;

        // Add the dropdown control, styling and event listener
        this._postalcodechange = this.PostalCodeChanged.bind(this);
        this._postCodePicklistElement = document.createElement("select");
        this._postCodePicklistElement.setAttribute("id", "adresslist");
        this._postCodePicklistElement.setAttribute("class", "pcfinputcontrol");
        this._postCodePicklistElement.setAttribute("height", "100px");
        this._postCodePicklistElement.setAttribute("weidth", "400px");
        this._container.appendChild(this._postCodePicklistElement);

        var i, L = this._postCodePicklistElement.options.length - 1;
        for (i = L; i >= 0; i--) {
            this._postCodePicklistElement.remove(i);
        }
        // this.addressDetails = new AddressResults(this.FetchAddressesByPostCode());
        this.FetchAddressesByPostCode();

    }

    /**
     * Called when any value in the property bag has changed. This includes field values, data-sets, global values such as container height and width, offline status, control metadata values such as label, visible, etc.
     * @param context The entire property bag available to control via Context Object; It contains values as set up by the customizer mapped to names defined in the manifest, as well as utility functions
     */
    public updateView(context: ComponentFramework.Context<IInputs>): void {
        // Add code to update control view
        var i, L = this._postCodePicklistElement.options.length - 1;
        for (i = L; i >= 0; i--) {
            this._postCodePicklistElement.remove(i);
        }
        this.FetchAddressesByPostCode();
        this._notifyOutputChanged();

    }

    /**
     * It is called by the framework prior to a control receiving new data.
     * @returns an object based on nomenclature defined in manifest, expecting object[s] for property marked as “bound” or “output”
     */
    public getOutputs(): IOutputs {

        return {


        };

    }
    /**
     * This function fetches the address (es) for the post code passed.
     * @returns a JSON object of address details data for the post code.
     */
    private FetchAddressesByPostCode(): any {
        var addressJson;
        var parameters = {
            PostCode: this._context.parameters.PostalCode.raw
        };
        var req = new XMLHttpRequest();
        req.open("POST", "/api/data/v9.2/sdds_AddressLookup", false);
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
                    var option;
                    var dropdown = document.getElementById('adresslist') as HTMLInputElement;
                    for (let i = 0; i < addressJson.results.length; i++) {
                        option = document.createElement('option');
                        option.text = addressJson.results[i].Address.AddressLine;
                        option.value = addressJson.results[i].Address.AddressLine;
                        //  dropdown.add(option);
                        dropdown.appendChild(option);
                    }

                } else {
                    console.log("Error calling the Address API");

                }
            }
        };
        req.send(JSON.stringify(parameters));
        return addressJson;

    }
    public PostalCodeChanged(evt: Event): void {
        this.FetchAddressesByPostCode();
        this._notifyOutputChanged();
    }
    /**
     * Called when the control is to be removed from the DOM tree. Controls should use this call for cleanup.
     * i.e. cancelling any pending remote calls, removing listeners, etc.
     */
    public destroy(): void {
        // Add code to cleanup control if necessary
    }


}
