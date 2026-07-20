
using Forge.Domain.Common;
namespace Forge.Domain.Entities{

    public sealed class Course : Entity{
        public string Name {get; private set; }
        public string Description {get; private set;}
        public bool IsActive {get; private set;}

        public Course(string name, string description, bool isActive=true){
            SetName(name);
            SetDescription(description);
            SetIsActive(isActive);
        }

        private void SetName(string name){
            if(string.IsNullOrWhiteSpace(name)){
                throw new ArgumentException("Name cannot be null or empty." , nameof(name));
            }

            Name = name;
        }


         private void SetDescription(string description){
            if(string.IsNullOrWhiteSpace(description)){
                throw new ArgumentException("Description cannot be null or empty." , nameof(description));
            }

            Description = description;
        }


        private void SetIsActive(bool isActive){
            IsActive = isActive;
        }

        public void Activate(){
            if(IsActive == true){
                throw new InvalidOperationException("Course is already Active");
            }    
            
            SetIsActive(true);
        }

        public void Deactivate(){
            if(IsActive == false){
                throw new InvalidOperationException("Course is already Inactive");
            }    
            
            SetIsActive(false);
        }
    }
}