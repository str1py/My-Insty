   M S B A M L   `   `   `  ���� N  JInstagram Assistant, Version=1.0.6.7, Culture=neutral, PublicKeyToken=nullC&clr-namespace:Instagram_Assistant.ViewInstagram_Assistant.View  R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R8clr-namespace:System.ComponentModel;assembly=WindowsBaseSystem.ComponentModel 6    0Instagram_Assistant.View.GeoStoriesWatchPageView   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     8local&clr-namespace:Instagram_Assistant.View     _ [Microsoft.Xaml.Behaviors, Version=1.1.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a3i+http://schemas.microsoft.com/xaml/behaviors  Z VFontAwesome.WPF, Version=4.7.0.37774, Culture=neutral, PublicKeyToken=0758b07a11a4f466-fa$http://schemas.fontawesome.io/icons/  Bscm8clr-namespace:System.ComponentModel;assembly=WindowsBase    � 	Resources     
FeedAction  BorderColor �� CornerRadius � RowDefinitions � ColumnDefinitions j� Stretch �� ImageSource!  FontAwesome.WPF.FontAwesome   Icon � Triggers :� RoutedEvent	 �� TargetProperty
 t� From	 t� To g� AccelerationRatio g� Duration t� EasingFunction0  *System.Windows.Media.Animation.ElasticEase7  1System.Windows.Media.Animation.EasingFunctionBase   
EasingMode   Oscillations   Springiness J� Effect3  -System.Windows.Media.Effects.DropShadowEffect   
BlurRadius   ShadowDepth  
FirstColor   Color  feedCollection �� Source �� Path �� SortDescriptions+  %System.ComponentModel.SortDescription   PropertyName   	Direction  	PanelShow �� 
TargetName  	PanelHide  	FlashAnim W� From	 W� To g� AutoReverse g� RepeatBehavior  	ColorAnim  SecondColor	  �� To��%  &	        7  7  &	     &	 \    &	 �    &	     &	 o    �� .R��� 8��  $ 5��._�� $	��150��$	��230�� �� �� �� �� �� �� �� �� �� �� �� �� �� �� ��  �� �� �� �� �� �� �� �� �� �� .+��� $��0��$��0��$��20��$��20��$
��Fill=����� *	PostImage+A� $��25��$��25��$��0��$��0��$��2��$��2��$��2��
����������� $ UniformToFill=� �� *AccountImage+�� $��2��$��2��$��10��$��5,0,0,0q�
�������$��12(�$��Center=�$��
ExtraLight"����� *AccountName+  $��0��$��12��$��3��$��10��$��Center=�$��25(�$
��0,10q�
������� �� *Action+ :� $ Loaded��[��� .���� .��t� $	 Margin�$
 
-100,0,0,0X�$ 0X�$	 0.9Q�$ 0:0:0.5D�   $ EaseOut=�$ 2��$ 7Q�   $ 20Q�$	 0.9Q�8  ��  �� $ Actions�   $
 Time��$ 
Descending=��� .��t� $	 Margin�$
 0,420,0,-420X�$ 0X�$ 	SidePanel��$ .9Q�$ 0:0:0.5D��� .��t� $	 Margin�$
 0X�$ 0,420,0,-420X�$ 	SidePanel��$ .9Q�$ 0:0:0.5D��� .��W� $ Status��$	 Opacity�$ 1X�$	 0.3X� . $ 0:0:1D�$ Forever���� .���� $ Status��$(	 "Foreground.(SolidColorBrush.Color)�$ Forever�� . $ 0:0:1D�#  �  .��� .+��  �� �� �� �� �� �� �� �� �� �� �� �� �� �� ��  �� �� �� �� �� �� �� �� �� �� �� �� �� �� �� �� �� �� �� �� �� �� �� �� �� �� �� �� �� �� �� �� .+��� $��0��$��32��$��4��$��8�� 	 TextBlockMainStyle#��� 	 $��Center=�$��Center=� 
 H1#��� 
 
Overview�� $��0��$��5��$��2��$��0��#��� 	   Regular#���  Last actions�� $��0��$��11��$��11��$��3��$ 3��
�������! J� Opacity$
! 0.05Q� -  'System.Windows.Media.Effects.BlurEffect  "   Radius$" 10Q�#   
KernelType$# Gaussian=��� $��21��$��15��$��11��$��3��$ 3��
�������$
! 0.05Q�   $" 10Q�$# Gaussian=��� $��11��$��14��$��4��$��3��$��Center=�$ �� Orientation$$ Vertical=�.+���   MainButtonStyle#���  $��40������ *StartCommand+���� *ButtonContent+�� -   % ��Name$% Status��$��Center=�
�������$��0,20,0,5q� 
 Bigger#���    TextBlockThinStyle#���  ���� *Stats.Status+*  $Microsoft.Xaml.Behaviors.Interaction&   Triggers& /  )Microsoft.Xaml.Behaviors.Core.DataTrigger  '   Value$' WORKING��:	  4Microsoft.Xaml.Behaviors.Core.PropertyChangedTrigger( 	  Binding( �� *Stats.Status+*
  $Microsoft.Xaml.Behaviors.TriggerBase) 
  Actions.) <  6Microsoft.Xaml.Behaviors.Media.ControlStoryboardAction  5  /Microsoft.Xaml.Behaviors.Media.StoryboardAction*   
Storyboard#* [ +   ControlStoryboardOption$
+ Play=�  $	' OFF��( �� *Stats.Status+.)   #* [ $
+ Stop=��� $��11��$��3��$��1��$��9��$��Center=�$$ 
Horizontal=�.+��� $��Center=�.+��� $$ 
Horizontal=�.+��� $$ Vertical=�$��0,0,30,0q�.+��� $$ 
Horizontal=�.+�  $��0,0,5,0q�$	 Eye=�$��15(�
�������$��Center=�$��Center=��� $��Center=�
�������$��20(�$��
ExtraLight"����� *Stats.SessionCount+�� $��Center=�$��
ExtraLight"�#��� 	 	session�� $$ Vertical=�$��0,0,40,0q�.+��� $$ 
Horizontal=�.+�  $��0,0,5,0q�$	 Eye=�$��15(�
�������$��Center=�$��Center=��� $��Center=�
�������$��20(�$��
ExtraLight"�#��� 	 ���� *Stats.Count+�� $��Center=�$��
ExtraLight"�#��� 	 total�� $��Center=�.+��� $��Center=�
�������$��20(�$��
ExtraLight"����� *Stats.NextIn+�� $��Center=�$��
ExtraLight"�#��� 	 	Next in�� $��11��$��3��$��22��$��9��$��Center=�$��Center=�$$ 
Horizontal=�.+��� $��Center=�$��0,0,40,0q�.+��� $��Center=�
�������$��20(�$)��#0,0,0,5                            q�$��
ExtraLight"����� *Stats.TimeInWork+�� $��Center=�$��
ExtraLight"�#��� 	 Time in work�� $��Center=�.+��� $��Center=�
�������$��20(�$��0,0,0,5q�$��
ExtraLight"����� *Stats.NextSessionIn+�� $��Center=�$��
ExtraLight"�#��� 	 Next session in�� $��4��$��0��$��32��$��2��$	! 0.6Q�#��� 	 #��� 
 $��Center=�$��Center=����� *LastActionTextHelper+�� $��1��$��8��$��0��$��32��
������ $��0q�#���   ��.  $��Hidden=�$��Hidden=�!, �� IsDeferredScrollingEnabled, .  ���� - �� Source#- [ ���� .R�$� . $� Orientation$. 
Horizontal=�� -   / �Name$/ 	SidePanel��$��0,420,0,-420q� �� �� �� �� �� �� �� �� �� �� �� �� �� �� ��  �� �� �� �� �� �� �� �� �� �� �� �� �� �� �� �� �� �� �� �� �� �� �� �� �� �� �� �� �� �� �� �� .+��� $��32��$��1��$��15��
���   �$	! 0.8Q�   $" 10Q�$# Gaussian=��� $��4��
������ $��0q�  FontAwesome.WPF.Awesome0   Content$0 LocationArrow=�$��14��#���  $��25(�y��� *StPanelShowVisibility+���� *StPanelShowCommand+ :� $ Button.Click��.[��� #���  �� $��4��
������ $��0q�$0 LocationArrow=�$��14��#���  $��25(�y��� *StPanelHideVisibility+���� *StPanelHideCommand+ :� $ Button.Click��.[��� #���  �� $��2��$��6��$��13��$��20��.+��� $��Search location���� $��9��$��2��$��10��$��8��1 �� TextAlignment$1 Center=�  MyWaterMarkStyle#���  $��30(�
2 � Tag$2 Enter loaction...������ *	GeoString+3 �� UpdateSourceTrigger$3 PropertyChanged=��� 
������ 
������ $	��300��$��Regular"�
�������  LogListStyle#���  ���� *SearchResults+4 �� SelectedItem4 �� *SelectedGeoItem+���� .R��� $��5q�$$ 
Horizontal=�.+�"  FontAwesome.WPF.ImageAwesome  5   Icon$5 LocationArrow=�$��0,0,5,0q�$��15��6   
Foreground
6 �����$
��Left=��� $��16(�$	��350��$
��Left=����� *Name+& +  %Microsoft.Xaml.Behaviors.EventTrigger  7   	EventName$7 MouseDoubleClick��.) 2  ,Microsoft.Xaml.Behaviors.InvokeCommandAction  8   Command8 �� *SelectGeoCommand+  $��8��$��15��$��2��$��2��$5 Spinner=�  SecondColorBrush#6 [ 9   Spin9 . $��50��$��50��y��� *LoadingVisibility+