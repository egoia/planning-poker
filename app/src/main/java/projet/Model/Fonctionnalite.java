package projet.Model;

public class Fonctionnalite {
    private String nom;
    private String description;
    private String note;

    public Fonctionnalite(String nom, String description, String note){
        this.nom = nom;
        this.description=description;
        this.note = this.note;
    }

    public String getNom() {
        return this.nom;
    }

    public void setNom(String nom) {
        this.nom = nom;
    }

    public String getDescription() {
        return this.description;
    }

    public void setDescription(String description) {
        this.description = description;
    }

    public String getNote() {
        return this.note;
    }

    public void setNote(AppManager.Card note) {
        this.note = note.toString();
    }

}
