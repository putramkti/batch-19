SomeDelegate d = SomeMethod1;
d += SomeMethod2;
// d = SomeMethod2;
d();
void SomeMethod1() { Console.WriteLine("Method 1"); }
void SomeMethod2() { Console.WriteLine("Method 2"); }
delegate void SomeDelegate();


// var sc = new SampleClass();

// Callback d = sc.InstanceMethod;
// d();

// // Map to the static method:
// d = SampleClass.StaticMethod;
// d();

// delegate void Callback();

// class SampleClass
// {
//     public void InstanceMethod()
//     {
//         Console.WriteLine("A message from the instance method.");
//     }

//     static public void StaticMethod()
//     {
//         Console.WriteLine("A message from the static method.");
//     }
// }



// var myCar = new Car { Name = "Tesla Model S" };

// Func<Vehicle> vehicleFactory = () => new Car { Name = "Porsche 911" };
// Vehicle newVehicle = vehicleFactory();
// Console.WriteLine($"[Func Covariance] Created: {newVehicle.Name}");

// Action<Car> carInspector = InspectVehicle;
// carInspector(myCar);

// void InspectVehicle(Vehicle vehicle)
// {
//     Console.WriteLine($"[Action Contravariance] Inspecting: {vehicle.Name}");
// }

// public class Vehicle
// {
//     public string Name { get; set; } = "Generic Vehicle";
// }

// public class Car : Vehicle
// {
// }