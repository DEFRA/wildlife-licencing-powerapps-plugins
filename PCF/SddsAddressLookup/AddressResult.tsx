export class  AddressResults {
    constructor(obj: any) {
      Object.assign(this, obj);
      this.AddressLine = obj.results[0].Address.AddressLine;
      this.BuildingNumber = obj.results[0].Address.BuildingNumber;
      this.Street = obj.results[0].Address.Street;
      this.Town = obj.results[0].Address.Town;
      this.County = obj.results[0].Address.County;
      this.Postcode = obj.results[0].Address.Postcode;
      this.Country = obj.results[0].Address.Country;
      this.UPRN = obj.results[0].Address.UPRN;
      
    }

    Results: Results[];

    AddressLine: string;
    BuildingNumber:string;
    Street:string;
    Town:string;
    County:string;
    Postcode:string;
    Country:string;
    XCoordinate:string;
    YCoordinate:string;
    UPRN:string;

  }

  export class  Results {
    constructor(obj: any) {
    }
  
  AddressList: Address;
  }

  export class  Address {
    constructor(obj: any) {
    }

    AddressLine: string;
    BuildingNumber:string;
    Street:string;
    Town:string;
    County:string;
    Postcode:string;
    Country:string;
    XCoordinate:string;
    YCoordinate:string;
    UPRN:string;
    
  }