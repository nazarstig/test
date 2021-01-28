using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VetClinic.DAL.Entities;

namespace VetClinic.DAL.Configurations
{
    public class ServicesConfiguration : IEntityTypeConfiguration<Service>
    {
        public void Configure(EntityTypeBuilder<Service> builder)
        {
            builder.HasKey(t => t.Id);
            builder.Property(t => t.ServiceName).HasMaxLength(50).IsRequired();
            builder.Property(t => t.Description).HasMaxLength(2000).IsRequired();

            builder
                .HasMany(t => t.Appointments)
                .WithOne(t => t.Service)
                .HasForeignKey(t => t.ServiceId);
               
            builder.HasData(
                new Service[]
                {
                    new Service{ Id=1, ServiceName="Inspection", Description=@"Basic medical examination of your animal by a fully equipped modern operating theatre and a pharmacy room. Our dental units provide facilities for all normal veterinary procedures. Diagnostic and surgical facilities include x-ray, ultrasound, electrocardiography, Doppler blood pressure, and a range of diagnostic endoscopes."},
                    new Service{ Id=2, ServiceName="Vacination", Description=@"We vaccinate and give booster inoculations according to an individual’s life style. We can do antibody titre tests to see if a dog actually needs a booster inoculation but rather than use in house antibody tests (that can produce false negatives), we use the gold standard and send all blood samples to the DEFRA approved specialist diagnostic laboratory used for rabies antibody titre analysis."},
                    new Service{ Id=3, ServiceName="Urgently", Description= @"During regular hospital hours, vet clinic can provide urgent care assistance. We will partner with you to determine whether referral to a specialty or emergency hospital is in your pet’s best interest. Depending on your pet’s individual needs and hospital capacity, assistance may consist of: 
                                                                Urgent care stabilization;
                                                                Referrals to specialty or emergency hospitals;
                                                                Laboratory testing and x-rays;
                                                                IV fluid therapy, pain control, infection treatments;
                                                                Wound and fracture care;
                                                                Treatment for poisonings or seizures"},
                    new Service{ Id=4, ServiceName="Surgery", Description= @"The anaesthetics and drugs we use during surgery together with the methods we use to monitor vital signs are comparable to those used for us when we have hospital operations. Continuous monitoring includes blood pressure, electrocardiogram, oxygen saturation, carbon dioxide levels, and ventilation. With the skill of our staff, it is ‘normal’ for us to successfully operate on very old, very sick animals that have a multitude of medical problems. While we never take the administration of a general anaesthetic casually, anaesthetic complications are rare despite the sometimes severe nature of the problems we manage."},
                    new Service{ Id=5, ServiceName="Internal Medicine", Description=@"Internal medicine services cover hormonal, gastrointestinal, urinary, haematologic (blood related), respiratory, infectious, and immune-mediated diseases. The facilities at the London Veterinary Clinic are co-ordinated by Grant Petrie. In addition to an extensive review of your pet’s medical history, diagnostics always begin with a thorough physical examination and blood tests. When more information is needed, ultrasound, radiography, endoscopy, and CT imaging may be used. Our objective is to make an accurate diagnosis as rapidly as possible while at the same time putting your pet (and you) to a minimum of inconvenience."},
                    new Service{ Id=6, ServiceName="Preventive Care", Description=@"We focus on preventive veterinary care to promote and improve overall pet health. Routine check-ups allow us to diagnose, treat and protect your pet from contracting serious, costly and sometimes fatal diseases. Offering a holistic approach to pet health, we partner with our clients to make sure their pets receive proper preventive"},
                    new Service{ Id=7, ServiceName="Cardiology", Description=@"Cardiology services are provided by Grant Petrie. In addition to an extensive medical history review, a consultation may include echocardiography, electrocardiography, radiography, blood pressure monitoring and 24 hour Holter monitoring to evaluate the cardiac status of your pet."},
                    new Service{ Id=8, ServiceName="Dermatology", Description=@"We will take a full medical history and may take an ear or skin swab or skin scrapings to help with the diagnosis. In rare and unusual skin conditions a skin biopsy may be needed. For allergic skin conditions exclusion diets are often recommended and allergy testing for inhaled allergies (pollens, moulds, dust mites etc) is done on blood samples sent to a specialist laboratory in The Netherlands. We have specialist referral veterinary dermatologist available for the most challenging cases."},
                    new Service{ Id=9, ServiceName="Dental Care", Description=@"Your pet's oral health is a good indicator of their overall health. Your pet's teeth should be checked at least once a year for early signs of problems. Our hospitals offer anesthetized dental cleanings and procedures."},
                    new Service{ Id=10, ServiceName="Diagnostic Imaging", Description=@"Among the most useful tools we use to evaluate a pet’s condition is diagnostic imaging. This includes radiographs (x-rays) and ultrasonography (ultrasound) both of which are carried out at York Street. We are able to refer pets for other forms of imaging if needed, this may include computed tomography (CT scans) and magnetic resonance imaging (MRI scans). Each of these provides different kinds of images, and in some instances more than one may be suggested by us to evaluate a particular problem in your pet. All of these methods provide ‘pictures’ of internal structures."}
                });
        }
    }
}
