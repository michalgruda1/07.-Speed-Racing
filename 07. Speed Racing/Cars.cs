using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _07.Speed_Racing
{
    class CarsCollection : IEnumerator,IEnumerable
    {
        private List<Car> cars;
        private int position = -1;

        public CarsCollection()
        {
            cars = new List<Car>();
        }

        public bool TryAdd(Car car)
        {
            // check if this model already was added
            if (cars.Exists(x => String.Equals(x.Model, car.Model)))
            {
                return false;
            }
            else
            {
                cars.Add(car);
                return true;
            }
        }

        public List<Car> FindCarByModel(string model)
        {
            return cars.FindAll(x => x.Model == model);
        }

        public int Count()
        {
            return cars.Count;
        }

        // methods for compliance with IEnumerator interface
        public bool MoveNext()
        {
            this.position++;
            return (this.position < cars.Count);
        }

        public void Reset()
        { position = 0; }

        public object Current
        {
            get { return cars[position]; }
        }
        // end IEnumerator compliance

        // method for IEnumerable compliance
        public IEnumerator GetEnumerator()
        {
            return (IEnumerator)this;
        }
    }

    class Car
    {
        public string Model { get; set; }
        // fuel consumption in liters per 1km
        public float FuelConsumptionFor1km { get; set; }
        private float fuelAmount;
        private float distanceTraveled;

        // 4 arg ctor
        public Car(string model, float fuelAmount, float fuelConsumptionFor1km, float distanceTraveled)
        {
            Model = model;
            FuelConsumptionFor1km = fuelConsumptionFor1km;
            this.fuelAmount = fuelAmount;
            this.distanceTraveled = distanceTraveled;
        }

        // 3 arg ctor - with 0km initial distanceTraveled
        public Car(string model, float fuelAmount, float fuelConsumptionFor1km) : this(model, fuelAmount, fuelConsumptionFor1km, 0F) { }

        public float GetFuelAmount()
        {
            // liters
            return fuelAmount;
        }

        public float GetDistanceTraveled()
        {
            // kilometers
            return distanceTraveled;
        }

        public bool TryTravelDistance(float distance)
        {
            // liters per kilometer 
            if (distance * this.FuelConsumptionFor1km < this.GetFuelAmount())
            {
                // yes, we can travel that far
                this.fuelAmount -= distance * FuelConsumptionFor1km;
                this.distanceTraveled += distance;
                return true;
            } else
            {
                return false;
            }
        }

        public override string ToString()
        {
            string str = String.Format("{0} {1} {2}", this.Model, this.GetFuelAmount(), this.GetDistanceTraveled());
            return str;
        }
    }
}
