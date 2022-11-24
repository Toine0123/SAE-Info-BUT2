using System;
using System.Drawing;
using System.Windows.Forms;

namespace SAE_INFO
{
    public partial class Form1 : Form
    {
        Case[] tabCase = new Case[40] {new Case(Type.DEPART),
                                       new Case.Propriete(60, new ColorC().MARRON),
                                       new Case(Type.EVENEMENT),
                                       new Case.Propriete(60, new ColorC().MARRON),
                                       new Case.Taxes(200),
                                       new Case.Gare(),
                                       new Case.Propriete(100, new ColorC().CYAN),
                                       new Case(Type.EVENEMENT),
                                       new Case.Propriete(100, new ColorC().CYAN),
                                       new Case.Propriete(120, new ColorC().CYAN),
                                       new Case(Type.PRISON),
                                       new Case.Propriete(140, new ColorC().ROSE),
                                       new Case(Type.COMPAGNIE),
                                       new Case.Propriete(140, new ColorC().ROSE),
                                       new Case.Propriete(160, new ColorC().ROSE),
                                       new Case.Gare(),
                                       new Case.Propriete(180, new ColorC().ORANGE),
                                       new Case(Type.EVENEMENT),
                                       new Case.Propriete(180, new ColorC().ORANGE),
                                       new Case.Propriete(200, new ColorC().ORANGE),
                                       new Case(Type.PARCGRATUIT),
                                       new Case.Propriete(220, new ColorC().ROUGE),
                                       new Case(Type.EVENEMENT),
                                       new Case.Propriete(220, new ColorC().ROUGE),
                                       new Case.Propriete(240, new ColorC().ROUGE),
                                       new Case.Gare(),
                                       new Case.Propriete(260, new ColorC().JAUNE),
                                       new Case.Propriete(260, new ColorC().JAUNE),
                                       new Case(Type.COMPAGNIE),
                                       new Case.Propriete(280, new ColorC().JAUNE),
                                       new Case(Type.ALLERENPRISON),
                                       new Case.Propriete(300, new ColorC().VERT),
                                       new Case.Propriete(300, new ColorC().VERT),
                                       new Case(Type.EVENEMENT),
                                       new Case.Propriete(320, new ColorC().VERT),
                                       new Case.Gare(),
                                       new Case(Type.EVENEMENT),
                                       new Case.Propriete(350, new ColorC().BLEU),
                                       new Case.Taxes(100),
                                       new Case.Propriete(400, new ColorC().BLEU)};
        Pion joueur;
        int resdes;
        public Form1()
        {
            joueur = new Pion();
            InitializeComponent();
        }

        private void buttonDes_Click(object sender, EventArgs e)
        {
            /*gestion du lancer de dé et avancer sur le plateaux*/
            Random rand = new Random();
            resdes = rand.Next(6);
            label1.Text = "Valeur du dés = " + resdes.ToString();
            joueur.Move(resdes);
            label1.Text += '\n' + "Numéro de Case = " + joueur.GetPosition();

            /*affichage en fonction de la case*/
            //reset des label en vue de l'affichage
            label3.Text = "";
            label3.Font = new Font(label3.Font, FontStyle.Regular);
            label4.Text = "";
            label4.Font = new Font(label4.Font, FontStyle.Regular);
            label5.Text = "";
            label5.Font = new Font(label5.Font, FontStyle.Regular);
            splitContainer1.Panel1.BackColor = Color.Gray;
            switch (tabCase[joueur.GetPosition()].GetTyp())
            {
                case Type.ALLERENPRISON:
                    label2.Text = "Aller en prison";
                    label3.Text = "Aller en prison, ne passer pas par la case départ";
                    break;
                case Type.COMPAGNIE:
                    label2.Text = "Compagnie";
                    break;
                case Type.DEPART:
                    label2.Text = "Départ";
                    break;
                case Type.EVENEMENT:
                    label2.Text = "Evenement";
                    break;
                case Type.GARE:
                    label2.Text = "Gare";
                    splitContainer1.Panel1.BackColor = Color.FromArgb(0, 0, 0);
                    break;
                case Type.PARCGRATUIT:
                    label1.Text = "Parc gratuit";
                    break;
                case Type.PRISON:
                    label1.Text = "Prison";
                    break;
                case Type.PROPRIETE:
                    label2.Text = "Propriété";
                    label3.Text = "Terrain nu     " + tabCase[joueur.GetPosition()].GetPrice();
                    label4.Text = "1 maison     " + (tabCase[joueur.GetPosition()].GetPrice() * 2);
                    label5.Text = "2 maisons     " + (tabCase[joueur.GetPosition()].GetPrice() * 3);
                    switch (tabCase[joueur.GetPosition()].GetLevel())
                    {
                        case 1:
                            label3.Font = new Font(label3.Font, FontStyle.Bold);
                            break;
                        case 2:
                            label4.Font = new Font(label4.Font, FontStyle.Bold);
                            break;
                        case 3:
                            label5.Font = new Font(label5.Font, FontStyle.Bold);
                            break;
                    }
                    if (((Case.Propriete)tabCase[joueur.GetPosition()]).color == new ColorC().ROUGE)
                    {
                        splitContainer1.Panel1.BackColor = new ColorC().ROUGE;
                    }
                    if (((Case.Propriete)tabCase[joueur.GetPosition()]).color == new ColorC().BLEU)
                        splitContainer1.Panel1.BackColor = new ColorC().BLEU;
                    if (((Case.Propriete)tabCase[joueur.GetPosition()]).color == new ColorC().CYAN)
                        splitContainer1.Panel1.BackColor = new ColorC().CYAN;
                    if (((Case.Propriete)tabCase[joueur.GetPosition()]).color == new ColorC().JAUNE)
                        splitContainer1.Panel1.BackColor = new ColorC().JAUNE;
                    if (((Case.Propriete)tabCase[joueur.GetPosition()]).color == new ColorC().MARRON)
                        splitContainer1.Panel1.BackColor = new ColorC().MARRON;
                    if (((Case.Propriete)tabCase[joueur.GetPosition()]).color == new ColorC().ORANGE)
                        splitContainer1.Panel1.BackColor = new ColorC().ORANGE;
                    if (((Case.Propriete)tabCase[joueur.GetPosition()]).color == new ColorC().ROSE)
                        splitContainer1.Panel1.BackColor = new ColorC().ROSE;
                    if (((Case.Propriete)tabCase[joueur.GetPosition()]).color == new ColorC().VERT)
                        splitContainer1.Panel1.BackColor = new ColorC().VERT;
                    break;
                case Type.TAXES:
                    label2.Text = "Taxe";
                    label3.Text = "Prix = " + tabCase[joueur.GetPosition()].GetPrice();
                    break;
            }
        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
