using System;
using System.Drawing;
using System.Reflection.Emit;
using System.Security;
using System.Windows.Forms;

/*************Problèmes**********
 *
 ********************************/
namespace SAE_INFO
{
    public partial class Form1 : Form
    {
        static Case[] plateau = new Case[40] {new Case(Type.DEPART, 0),
                                       new Case.Propriete(new ColorC().MARRON, 1),
                                       new Case(Type.EVENEMENT, 2),
                                       new Case.Propriete(new ColorC().MARRON, 3),
                                       new Case.Taxes(200, 4),
                                       new Case.Gare(5),
                                       new Case.Propriete(new ColorC().CYAN, 6),
                                       new Case(Type.EVENEMENT, 7),
                                       new Case.Propriete(new ColorC().CYAN, 8),
                                       new Case.Propriete(new ColorC().CYAN, 9),
                                       new Case(Type.PRISON, 10),
                                       new Case.Propriete(new ColorC().ROSE, 11),
                                       new Case(Type.COMPAGNIE, 12),
                                       new Case.Propriete(new ColorC().ROSE, 13),
                                       new Case.Propriete(new ColorC().ROSE, 14),
                                       new Case.Gare(15),
                                       new Case.Propriete(new ColorC().ORANGE, 16),
                                       new Case(Type.EVENEMENT, 17),
                                       new Case.Propriete(new ColorC().ORANGE, 18),
                                       new Case.Propriete(new ColorC().ORANGE, 19),
                                       new Case(Type.PARCGRATUIT, 20),
                                       new Case.Propriete(new ColorC().ROUGE, 21),
                                       new Case(Type.EVENEMENT, 22),
                                       new Case.Propriete(new ColorC().ROUGE, 23),
                                       new Case.Propriete(new ColorC().ROUGE, 24),
                                       new Case.Gare(25),
                                       new Case.Propriete(new ColorC().JAUNE, 26),
                                       new Case.Propriete(new ColorC().JAUNE, 27),
                                       new Case(Type.COMPAGNIE, 28),
                                       new Case.Propriete(new ColorC().JAUNE, 29),
                                       new Case(Type.ALLERENPRISON, 30),
                                       new Case.Propriete(new ColorC().VERT, 31),
                                       new Case.Propriete(new ColorC().VERT, 32),
                                       new Case(Type.EVENEMENT, 33),
                                       new Case.Propriete(new ColorC().VERT, 34),
                                       new Case.Gare(35),
                                       new Case(Type.EVENEMENT, 36),
                                       new Case.Propriete(new ColorC().BLEU, 37),
                                       new Case.Taxes(100, 38),
                                       new Case.Propriete(new ColorC().BLEU, 39)};
        static Pion[] joueurs = new Pion[4] {new Pion("joueur 1", Color.Black, plateau[0]),
                                      new Pion("joueur 2", Color.Blue, plateau[0]),
                                      new Pion("balkani", Color.Green, plateau[0]),
                                      new Pion("joueur 4", Color.Red, plateau[0])};
        Afficheur afficheur = new Afficheur(joueurs);
        int resdes;
        int joueur = 0;



        static Evenement.deplacement[] evenement_1 = new Evenement.deplacement[4]
        {
                new Evenement.deplacement("avancer de 3 cases",1,3),
                new Evenement.deplacement("reculer de 3 cases",2,-3),
                new Evenement.deplacement("avancer de 1 case",3,1),
                new Evenement.deplacement("reculer de 1 case",4,-1),
        };

        static Evenement.teleportation[] evenement_2 = new Evenement.teleportation[10]
        {
                new Evenement.teleportation("aller jusqu'à la gare Monparnasse",5,5),
                new Evenement.teleportation("aller jusqu'à la gare de Lyon",6,15),
                new Evenement.teleportation("aller jusqu'à la gare du Nord",7,25),
                new Evenement.teleportation("aller jusqu'à la gare de Saint-Lazare",8,35),
                new Evenement.teleportation("aller jusqu'à l'avenue Henry-Martin",9,24),
                new Evenement.teleportation("aller jusqu'à la rue de la paix",10,39),
                new Evenement.teleportation("aller jusqu'à la compagnie d'élèctricité",11,11),
                new Evenement.teleportation("aller jusqu'à la gare des eaux",12,28),
                new Evenement.teleportation("aller jusqu'au parc gratuit",22,20),
                new Evenement.teleportation("aller jusqu'à Boulevard de la villette",13,12),
        };
        static Evenement.valeur[] evenement_3 = new Evenement.valeur[8]
        {
                new Evenement.valeur("joyeux anniversaire chaque joueur vous verse 10 $",14,10),
                new Evenement.valeur("vous avez été élue président du conseille verser 50 $ à chaque joueur",15,50),
                new Evenement.valeur("votre prêt et votre immeuble vous rapport recevez 150 $",16,150),
                new Evenement.valeur("vous avez été second dans un concour de prix de beauté recever 10 $",17,10),
                new Evenement.valeur("frais médicaux payer 100 $",18,100),
                new Evenement.valeur("frais de scolarité payer 50 $",19,50),
                new Evenement.valeur("excés de vitesse payer 15 $",20,15),
                new Evenement.valeur("la banque vous verse des dividende recevez 100 $",21,100),
        };

        int choose =0;
        int chance =0;



public Form1()
        {
            for(int i = 0; i < 40; i++)
            {
                plateau[i].setPos(i);
                plateau[i].Click += Case_Click;
                Controls.Add(plateau[i]);
            }
            for(int i = 0; i < 4; i++)
            {
                Controls.Add(joueurs[i]);
                joueurs[i].BringToFront();
            }
            Controls.Add(afficheur);
            InitializeComponent();
            HideEvent();
        }

        private void Case_Click(object sender, EventArgs e)
        {
            affichageCase(((Case)sender).getPos());
        }

        private void buttonDes_Click(object sender, EventArgs e)
        {
            HideEvent();
            while (joueurs[joueur].IsLoose())
            {
                joueur ++;
                if (joueur > 3) joueur = 0;
            }
            label6.Text = "tour de joueur " + (joueur + 1) + "\n";
            /*gestion du lancer de dé et avancer sur le plateaux*/
            Random rand = new Random();
            do
            {
                resdes = rand.Next(12);
            } while (resdes < 2 || resdes > 12);
            label1.Text = "Valeur du dés = " + resdes.ToString();
            label1.Text += "\nArgent avant déplacement = " + joueurs[joueur].GetMoney();
            joueurs[joueur].Move(resdes, plateau);
            label1.Text += "\nArgent après déplacement = " + joueurs[joueur].GetMoney();
            label1.Text += '\n' + "Numéro de Case = " + joueurs[joueur].GetPosition();

            affichageCase(joueurs[joueur].GetPosition());
            
            //gestion d'évènement 
            if (plateau[joueurs[joueur].GetPosition()].isevent() == true)
            {
                choose = rand.Next(2)+1;
                switch (choose)
                {
                    case 1 :
                        chance = rand.Next(3);

                        //affichage de l'évent
                        ShowEvent();
                        label7.Text = evenement_1[chance].GetText();
                        
                        //gestion event avancer ou reculer 
                        joueurs[joueur].Move(evenement_1[chance].GetMove(), plateau);

                        break;
                    
                    case 2:
                        chance = rand.Next(9);

                        //affichage de l'évent
                        ShowEvent();
                        label7.Text = evenement_2[chance].GetText();

                        //gestion de l'évent aller à ...
                        evenement_2[chance].AllerVers(joueurs[joueur],plateau);

                        break;
                    case 3:
                        chance = rand.Next(7);

                        //affichage de l'évent
                        ShowEvent();
                        label7.Text = evenement_3[chance].GetText();

                        //gestion event anniversaire 
                        if (evenement_3[chance].GetId() == 14)
                        {
                            evenement_3[chance].anniversaire(joueurs, joueur);
                        }
                        //gestion event president 
                        if (evenement_3[chance].GetId() == 15)
                        {
                            evenement_3[chance].president(joueurs, joueur);
                        }
                        //gestion event ajout d'argent
                        else 
                        {
                            joueurs[joueur].AddMoney(evenement_3[chance].GetVal());
                        }
                        break;
                        
                }
            }
            //achat de la propriété
            if (plateau[joueurs[joueur].GetPosition()].isBuyable() == true)
            {
                joueurs[joueur].Buy(plateau[joueurs[joueur].GetPosition()]);
            }
            label1.Text += "\nArgent en fin de tour = " + joueurs[joueur].GetMoney();
            joueurs[joueur].IsLoose();
            joueur += 1;
            if(joueur>3) joueur = 0;
            label6.Text += "joueur suivant : " + (joueur + 1);
            afficheur.MAJ();
        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void panel11_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void splitContainer13_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void splitContainer3_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }
        private void HideEvent()
        {
            label7.Hide();
            label8.Hide();
            splitContainer2.Hide();
        }
        private void ShowEvent() 
        {
            label7.Show();
            label8.Show();
            splitContainer2.Show();
        }
        private void affichageCase(int pos)
        {
            /*affichage en fonction de la case*/
            //reset des label en vue de l'affichage
            label3.Text = "";
            label3.Font = new Font(label3.Font, FontStyle.Regular);
            label4.Text = "";
            label4.Font = new Font(label4.Font, FontStyle.Regular);
            label5.Text = "";
            label5.Font = new Font(label5.Font, FontStyle.Regular);
            splitContainer1.Panel1.BackColor = Color.Gray;
            //action en fonction de ce qui c'est passer
            switch (plateau[pos].GetTyp())
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
                    label3.Text = "Terrain nu     " + plateau[pos].GetTabPrice()[1];
                    label4.Text = "1 maison     " + (plateau[pos].GetTabPrice()[2]);
                    label5.Text = "2 maisons     " + (plateau[pos].GetTabPrice()[3]);
                    label5.Text += "\nIsBuildable = " + ((Case.Propriete)plateau[pos]).IsBuildable();
                    switch (plateau[pos].GetLevel())
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
                    splitContainer1.Panel1.BackColor = plateau[pos].GetColor();
                    break;
                case Type.TAXES:
                    label2.Text = "Taxe";
                    label3.Text = "Prix = " + plateau[pos].GetPrice();
                    break;
            }
        }

       
    }
}
